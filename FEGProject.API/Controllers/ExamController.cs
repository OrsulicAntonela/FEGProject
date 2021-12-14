using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using FEGProjectData.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using FEGProject.API.Models;
using FEGProjectData.Entities;
using FEGProject.API.Infrastructure.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FEGProject.API.Controllers
{
    [Route("api/exam")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ExamRepository _examRepository;
        private readonly StudentRepository _studentRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ExamController> _logger;

        public ExamController(IMapper mapper, ExamRepository examRepository, StudentRepository studentRepository, IConfiguration configuration, ILogger<ExamController> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
            _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{examId}")]
        public IActionResult GetExamWithQuestions(int examId)
        {
            _logger.LogInformation("Hello, world!");
            var exam = _examRepository.GetExamWithQuestions(examId);
            
            if (exam == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ExamDto>(exam));
        }

        [HttpPost]
        public IActionResult CreateExam([FromBody] ExamDto exam)
        {
            var examEntity = _mapper.Map<Exam>(exam);

            _examRepository.AddExam(examEntity);

            return Ok(_mapper.Map<ExamDto>(examEntity));
        }

        [HttpDelete ("{examId}")]
        public IActionResult DeleteExam(int examId, [FromBody] AuthorizationDto authorization)
        {
            if (authorization.Password != _configuration["PasswordStrings:ProfessorPassword"])
            {
                return new UnauthorizedObjectResult(authorization);
            }

            _examRepository.DeleteExam(examId);

            return Ok();
        }

        [HttpGet("{examId}/assignexam/{assignedExamId}")]
        public IActionResult GetAssignedExam(int assignedExamId)
        {
            var assignedExam = _examRepository.GetAssignedExam(assignedExamId);
            
            if (assignedExam == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AssignedExamDto>(assignedExam));
        }

        [HttpGet("{examId}/assignexam")]
        public IActionResult GetActiveAssignedExams()
        {
            var assignedExam = _examRepository.GetActiveAssignedExams();

            if (assignedExam == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<AssignedExamDto>>(assignedExam));
        }

        [HttpPost("{examId}/assignexam")]
        public IActionResult CreateAssignExam(int examId, [FromBody] AssignedExamDto assignedExam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var assignedExamEntity = _mapper.Map<AssignedExam>(assignedExam);

            _examRepository.AddAssignedExam(examId, assignedExamEntity);

            return Ok(_mapper.Map<AssignedExamDto>(assignedExamEntity));
        }

        [HttpPost("{examId}/assignexam/{assignedExamId}")]
        public IActionResult AssignExamForGroups(int assignedExamId, [FromBody] List<GroupDto> groups)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var assignedExamEntity = _examRepository.GetAssignedExam(assignedExamId);

            if (assignedExamEntity==null)
            {
                return NotFound();
            }

            foreach (var groupDto in groups)
            {
                var group = _studentRepository.GetGroup(groupDto.GroupId);

                if (group != null && !assignedExamEntity.Groups.Any(g => g.GroupId == groupDto.GroupId))
                { 
                    assignedExamEntity.Groups.Add(group);

                    //Assigning exam for each student
                    var studentsInGroup = group.Students;

                    foreach(var student in studentsInGroup)
                    {
                        var studentAssignedExam = new StudentAssignedExam
                        {
                            StudentId = student.StudentId,
                            AssignedExamId = assignedExamId
                        };
                        _examRepository.AddStudentAssignedExam(studentAssignedExam);
                    }
                }
            }

            return Ok(_mapper.Map<AssignedExamDto>(assignedExamEntity));
        }

        [HttpDelete("{examId}/assignexam/{assignedExamId}")]
        public IActionResult DeleteAssignedExam(int assignedExamId, [FromBody] AuthorizationDto authorization)
        {
            if (authorization.Password != _configuration["PasswordStrings:ProfessorPassword"])
            {
                return new UnauthorizedObjectResult(authorization);
            }

            _examRepository.DeleteAssignedExam(assignedExamId);

            return Ok();
        }

        [HttpGet("{examId}/student/{studentId}")]
        public IActionResult GetExtendedExam(int examId, int studentId)
        {
            var studentAssignedExamEntity = _examRepository.GetStudentAssignedExam(examId,studentId);

            if(studentAssignedExamEntity==null)
            {
                return NotFound();
            }

            if(studentAssignedExamEntity.StudentAnswers.Count!=0 && studentAssignedExamEntity.Result == null)
            {
                //Odgovoreno je čeka se ocjena
                return NoContent();
            }

            if(studentAssignedExamEntity.Result!=null)
            {
                var result = studentAssignedExamEntity.Result;
                //Ocjena je unešena
                return NoContent();
            }

            if(DateTime.Now > studentAssignedExamEntity.AssignedExam.Deadline)
            {
                //Isteklo vrijeme ne može se više pristupiti
                return NoContent();
            }

            var examEntity = studentAssignedExamEntity.AssignedExam.Exam;
            var exam = _mapper.Map<ExamExtendedDto>(examEntity);

            exam.Deadline = examEntity.AssignedExams.Select(a => a.Deadline).FirstOrDefault();

            return Ok(exam);
        }

        [HttpGet("{examId}/student")]
        public IActionResult GetResultsOfExam(int examId)
        {
            var studentAssignedExamEntityList = _examRepository.GetListOfStudentAssignedExam(examId);
            
            if(studentAssignedExamEntityList.Count==0)
            {
                return NotFound();
            }
             
            return Ok(_mapper.Map<List<StudentAssignedExamDto>>(studentAssignedExamEntityList));
        }

        [HttpPost("{examId}/student/{studentId}")]
        public IActionResult SaveAnswersFromExam(int examId, int studentId, [FromBody] ExamExtendedDto exam)
        {
            var studentAssignedExamEntity= _examRepository.GetStudentAssignedExam(examId, studentId);
            var questionsWitoutAnswers = new List<int>();
            studentAssignedExamEntity.DeliveryTime = DateTime.Now;

            if (studentAssignedExamEntity == null)
            {
                return NotFound();
            }

            if (studentAssignedExamEntity.DeliveryTime > studentAssignedExamEntity.AssignedExam.Deadline)
            { 
                //Isteklo je vrijeme ne može se predati
                return NoContent();
            }

            if(studentAssignedExamEntity.StudentAnswers.Count>0)
            {
                //Ispit je vec odgovoren
                return NoContent();
            }

            // Looking for unanswered questions
            foreach (var question in exam.Questions)
            {
                if (question.Type == 1 && string.IsNullOrEmpty(question.AnswerText))
                {
                    questionsWitoutAnswers.Add(question.QuestionId);
                }
                else if(question.Type!=1 && question.QuestionOptions.Count == 0)
                {
                    questionsWitoutAnswers.Add(question.QuestionId);
                }
            }

            if (questionsWitoutAnswers.Count>0)
            {
                return Ok(questionsWitoutAnswers);
            }

            //Saving answers
            foreach (var question in exam.Questions)
            {
                var studentAnswer = new StudentAnswerDto
                {
                    StudentAssignedExamId = studentAssignedExamEntity.StudentAssignedExamId,
                    QuestionId = question.QuestionId
                };

                if(question.Type == 1)
                {
                    studentAnswer.Answer = question.AnswerText;
                }
                else
                {
                    var option = string.Join(",", question.QuestionOptions.Select(o=>o.Option).ToList());
                    studentAnswer.Answer = option;
                }

                var studentAnswerEntity = _mapper.Map<StudentAnswer>(studentAnswer);
                _examRepository.AddStudentAnswer(studentAnswerEntity);
            }
            return Ok();
        }

        [HttpGet("{examId}/assignedExam/{assignedExamId}/student/{studentId}")] 
        public IActionResult GetExamWithAnswers(int examId, int studentId)
        {
            var studentAssignedExamEntity = _examRepository.GetStudentAssignedExam(examId, studentId);
           
            if (studentAssignedExamEntity == null)
            {
                return NotFound();
            }

            var studentAnswerEntity = _examRepository.GetStudentAnswers(studentAssignedExamEntity.StudentAssignedExamId);

            if (studentAnswerEntity == null)
            {
                return NotFound();
            }

            return Ok(ExamHelper.MappingExamToExamWithAnswers(_mapper, studentAssignedExamEntity, studentAnswerEntity));
        }


        [HttpPost("{examId}/assignedExam/{assignedExamId}/student/{studentId}")]
        public IActionResult AddOrEditResult(int examId, int studentId, [FromBody]StudentAssignedExamDto studentAssignedExam)
        {
            var studentAssignedExamEntity = _examRepository.GetStudentAssignedExamToEditResult(examId, studentId);
            
            if(studentAssignedExamEntity == null)
            {
                return NotFound();
            }
             
            if(studentAssignedExamEntity.StudentAnswers.Count == 0)
            {
                return NotFound();
            }

            studentAssignedExamEntity.Result = studentAssignedExam.Result;
            _examRepository.Save();

            return Ok();
        }

        [HttpDelete("{examId}/student/{studentId}/assignedExam/{assignedExamId}/question/{questionId}")]
        public IActionResult DeleteStudentAnswerForQuestion(int studentId, int questionId, [FromBody] AuthorizationDto authorization)
        {
            if (authorization.Password != _configuration["PasswordStrings:ProfessorPassword"])
            {
                return new UnauthorizedObjectResult(authorization);
            }

            _examRepository.DeleteStudentAnswer(studentId, questionId);

            return Ok();
        }
    }
}
