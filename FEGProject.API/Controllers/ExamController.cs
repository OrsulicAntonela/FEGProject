using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FEGProjectData.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEGProject.API.Models;
using FEGProjectData.Entities;

namespace FEGProject.API.Controllers
{
    [Route("api/exam")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ExamRepository _examRepository;

        public ExamController(IMapper mapper, ExamRepository examRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
        }

        [HttpGet("{examId}", Name = "GetExam")]
        public IActionResult GetExamWithQuestions(int examId)
        {
            var exam = _examRepository.GetAllQuestionsOfExam(examId);
            return Ok(_mapper.Map<ExamDto>(exam));
        }
        [HttpPost]
        public IActionResult CreateExam([FromBody] ExamForCreationDto exam)
        {
            var examEntity = _mapper.Map<Exam>(exam);
            _examRepository.AddExam(examEntity);

            //var createdExam = _mapper.Map<ExamDto>(examEntity);
            return CreatedAtRoute("GetExam", new { examId = examEntity.ExamId }, examEntity);
        }

        [HttpPost("{examId}/assignexam")]
        public IActionResult AssignExam(int examId, [FromBody] AssignedExamForCreationDto assignedExam)
        {
            var assignedExamEntity = _mapper.Map<AssignedExam>(assignedExam);
            _examRepository.AddAssignedExam(examId, assignedExamEntity);
            return NoContent();
        }

        [HttpPost("{examId}/assignexam/{assignedExamId}")]
        public IActionResult AssignExamForGroups(int examId, int assignedExamId, [FromBody] AssignedExamForCreationDto assignedExam)
        {
            
            return NoContent();
        }
    }
}
