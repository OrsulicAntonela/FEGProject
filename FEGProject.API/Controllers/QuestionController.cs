using AutoMapper;
using FEGProject.API.Models;
using FEGProjectData.Entities;
using FEGProjectData.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProject.API.Controllers
{
    [Route("api/exam/{examId}/question")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly QuestionRepository _questionRepository;

        public QuestionController(IMapper mapper, QuestionRepository questionRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        }

        [HttpGet("{questionId}", Name ="GetQuestion")]
        public IActionResult GetQuestion(int examId, int questionId)
        {
            if (!_questionRepository.QuestionExists(examId, questionId))
            {
                return NotFound();
            }

            var question = _questionRepository.GetQuestion(examId, questionId);

            if (question == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<QuestionDto>(question));
        }

        [HttpPost]
        public IActionResult CreateQuestion(int examId, [FromBody] QuestionForCreationDto question)
        {
            if (string.IsNullOrEmpty(question.Text))
            {
                ModelState.AddModelError(
                    "Text",
                    "Text can't be empty");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var questionEntity = _mapper.Map<Question>(question);

            _questionRepository.AddQuestion(examId, questionEntity);

            var createdQuestion = _mapper.Map<QuestionDto>(questionEntity);

            return CreatedAtRoute("GetQuestion", new { examId, questionId = createdQuestion.QuestionId }, createdQuestion);
        }

        [HttpPost("edit/{questionId}")]
        public IActionResult EditQuestion(int examId, int questionId, [FromBody] QuestionForEditDto question)
        {
            var questionEntity = _questionRepository.GetQuestion(examId, questionId);
            _mapper.Map(question, questionEntity);
            _questionRepository.Save();
            return NoContent();
        }
    }
}
