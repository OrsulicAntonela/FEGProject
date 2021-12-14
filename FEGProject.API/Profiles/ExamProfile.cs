using AutoMapper;
using FEGProject.API.Models;
using FEGProjectData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProject.API.Profiles
{
    public class ExamProfile : Profile
    {
        public ExamProfile()
        {
            
            CreateMap<QuestionOption, QuestionOptionDto>();
            CreateMap<QuestionOptionDto, QuestionOption>();

            CreateMap<QuestionDto, Question>().ForMember(q => q.QuestionId, e => e.Ignore());
            CreateMap<Question, QuestionDto>();
            CreateMap<Question, QuestionExtendedDto>();
            CreateMap<QuestionExtendedDto, Question>();

            CreateMap<Exam, ExamDto>().ReverseMap();
            CreateMap<Exam, ExamExtendedDto>().ReverseMap();

            CreateMap<AssignedExam, AssignedExamDto>();
            CreateMap<AssignedExamDto, AssignedExam>();

            CreateMap<StudentAssignedExam, StudentAssignedExamDto>();
            CreateMap<StudentAssignedExamDto, StudentAssignedExam>();

            CreateMap<StudentAnswerDto, StudentAnswer>();
        }
    }
}
