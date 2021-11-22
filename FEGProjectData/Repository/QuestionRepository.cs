using FEGProjectData.Contexts;
using FEGProjectData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
        public Question GetQuestion(int examId, int questionId)
        {
            return _context.Questions
                .Include(q => q.QuestionOption)
                .Where(e => e.ExamId == examId)
                .Where(q => q.QuestionId == questionId)
                .FirstOrDefault();
        }
        public bool QuestionExists(int examId, int questionId)
        {
            return _context.Questions.Where(e => e.ExamId == examId).Any(q => q.QuestionId == questionId);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
