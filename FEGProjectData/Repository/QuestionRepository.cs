using FEGProjectData.Contexts;
using FEGProjectData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


namespace FEGProjectData.Repository
{
    public class QuestionRepository
    {
        private readonly FEGProjectContext _context;
        public QuestionRepository(FEGProjectContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void AddQuestion(int examId, Question question)
        {
            question.ExamId = examId;
            _context.Questions.Add(question);
            Save();
        }
        public Question GetQuestion(int questionId)
        {
            return _context.Questions.Include(q => q.QuestionOptions)
                .Where(q => q.QuestionId == questionId)
                .FirstOrDefault();
        }
        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
