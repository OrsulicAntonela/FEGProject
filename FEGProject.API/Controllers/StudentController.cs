using AutoMapper;
using FEGProject.API.Models;
using FEGProjectData.Entities;
using FEGProjectData.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FEGProject.API.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class StudentController: ControllerBase
    {
        private readonly StudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentController(StudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _studentRepository.GetAll();

            if (students == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<StudentDto>>(students));
        }

        [HttpGet("{studentId}")]
        public IActionResult GetStudent(int studentId)
        {
            var student = _studentRepository.GetStudent(studentId);
            
            if(student == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<StudentDto>(student));
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody] StudentDto student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var studentEntity = _mapper.Map<Student>(student);
            
            _studentRepository.AddStudent(studentEntity);

            return Ok(_mapper.Map<StudentDto>(studentEntity));
        }

        [HttpGet("{studentId}/results")]
        public IActionResult GetStudentResults(int studentId)
        {
            var studentAssignedExamEntity = _studentRepository.GetResultsForStudent(studentId);

            if (studentAssignedExamEntity == null || studentAssignedExamEntity.Count == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<StudentAssignedExamDto>>(studentAssignedExamEntity));

        }

        [HttpGet("group/{groupId}")]
        public IActionResult GetGroupOfStudents(int groupId)
        {
            var students = _studentRepository.GetByGroupId(groupId);

            if (students == null || students.Count == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<StudentDto>>(students));
        }

        [HttpGet("group/results")]
        public IActionResult GetAverageResults()
        {
            var groupsEntity = _studentRepository.GetGroups(); 
            var averageResultForGroups = new List<AverageResultForGroupDto>();

            if (groupsEntity == null)
            {
                return NotFound();
            }

            foreach (var group in groupsEntity)
            {
                var averageResultForGroup = new AverageResultForGroupDto
                {
                    GroupName = group.Name,
                    AverageResultForExams = new List<AverageResultForExamsDto>()
                };

                averageResultForGroup.AverageResultForExams = group.Students
                    .SelectMany(s => s.StudentAssignedExams)
                    .GroupBy(g => g.AssignedExam)
                    .Select(f => new AverageResultForExamsDto
                    {
                        ExamName = f.Key.Exam.Name,
                        AverageResult = AverageResult(f.Select(x => (double)x.Result).ToList(), f.Count())
                    }
                    )
                    .ToList();

                averageResultForGroups.Add(averageResultForGroup);
            }

            return Ok(averageResultForGroups);
        }

        private static double AverageResult(List<double> results, int count)
        {
            double sum = 0;

            foreach(var result in results)
            {
                sum += result;
            }

            return sum/count;
        }

        [HttpPost("group")]
        public IActionResult CreateGroup([FromBody] GroupDto group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var groupEntity = _mapper.Map<Group>(group);
            
            _studentRepository.AddGroup(groupEntity);

            return Ok(_mapper.Map<GroupDto>(groupEntity));
        }
    }
}
