using AutoMapper;
using FEGProject.API.Models;
using FEGProjectData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProject.API.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            
            CreateMap<QuestionOption, QuestionOptionDto>().ReverseMap();
            CreateMap<QuestionOption, QuestionOptionForEditDto>().ReverseMap();
            CreateMap<Question, QuestionForCreationDto>().ReverseMap();
            CreateMap<Question, QuestionForEditDto>().ReverseMap();
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<Exam, ExamForCreationDto>().ReverseMap();
            CreateMap<Exam, ExamDto>().ReverseMap();
            CreateMap<AssignedExamForCreationDto, AssignedExam>();
        }
    }
}
