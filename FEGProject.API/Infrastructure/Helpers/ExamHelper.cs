using AutoMapper;
using FEGProject.API.Models;
using FEGProjectData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProject.API.Infrastructure.Helpers
{
    public static class ExamHelper
    {
        public static ExamExtendedDto MappingExamToExamWithAnswers(IMapper mapper, StudentAssignedExam studentAssignedExamEntity, List<StudentAnswer> studentAnswerEntity)
        {
            var examWithAnswers = new ExamExtendedDto
            {
                ExamId = studentAssignedExamEntity.AssignedExam.ExamId,
                Deadline = studentAssignedExamEntity.AssignedExam.Deadline,
                Name = studentAssignedExamEntity.AssignedExam.Exam.Name,
                Questions = new List<QuestionExtendedDto>()
            };

            foreach (var questionAnswer in studentAnswerEntity)
            {
                if (questionAnswer.Question.Type == 1)
                {
                    var question = mapper.Map<QuestionExtendedDto>(questionAnswer.Question);
                    question.AnswerText = questionAnswer.Answer;
                    examWithAnswers.Questions.Add(question);
                }
                else
                {
                    var question = new QuestionExtendedDto
                    {
                        QuestionId = (int)questionAnswer.QuestionId,
                        Text = questionAnswer.Question.Text,
                        Type = questionAnswer.Question.Type,
                        QuestionOptions = new List<QuestionOptionDto>()
                    };

                    string[] optionsArray = questionAnswer.Answer.Split(",");

                    foreach (var optionsString in optionsArray)
                    {
                        var option = Convert.ToInt32(optionsString);

                        var questionOption = questionAnswer.Question.QuestionOptions.Where(q => q.Option == option).FirstOrDefault();
                        question.QuestionOptions.Add(mapper.Map<QuestionOptionDto>(questionOption));
                    }
                    examWithAnswers.Questions.Add(question);
                }
            }

            return examWithAnswers;
        }
    }
}
