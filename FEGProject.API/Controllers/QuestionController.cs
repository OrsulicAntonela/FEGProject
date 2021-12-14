using AutoMapper;
using FEGProject.API.Models;
using FEGProjectData.Entities;
using FEGProjectData.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

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

        [HttpGet("{questionId}")]
        public IActionResult GetQuestion(int questionId)
        {
            var question = _questionRepository.GetQuestion(questionId);

            if (question == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<QuestionDto>(question));
        }

        [HttpPost]
        public IActionResult CreateQuestion(int examId, [FromBody] QuestionDto question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (question.Type == 1)
            {
                question.QuestionOptions.Clear();
            }
            else
            {
                if(question.QuestionOptions==null || question.QuestionOptions.Count==0 || question.QuestionOptions.Count == 1)
                {
                    return BadRequest();
                }
            }
            var questionEntity = _mapper.Map<Question>(question);

            _questionRepository.AddQuestion(examId, questionEntity);

            return Ok(_mapper.Map<QuestionDto>(questionEntity));
        }

        [HttpPost("edit/{questionId}")]
        public IActionResult EditQuestion(int questionId, [FromBody] QuestionDto question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionEntity = _questionRepository.GetQuestion(questionId);

            if (questionEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(question, questionEntity);
            _questionRepository.Save();

            return Ok();
        }
    }
}
