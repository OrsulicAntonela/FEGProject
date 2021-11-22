using FEGProjectData.Contexts;
using FEGProjectData.Entities;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEGProjectData.Repository
{
    public class ExamRepository
    {
        private readonly FEGProjectContext _context;
        public ExamRepository(FEGProjectContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Exam GetAllQuestionsOfExam(int examId)
        {
            return _context.Exams
                .Include(q => q.Question)
                .ThenInclude(qo => qo.QuestionOption)
                .Where(e => e.ExamId == examId)
                .FirstOrDefault();
        }
        public void AddExam(Exam exam)
        {
            _context.Exams.Add(exam);
            _context.SaveChanges();
        }

        public void AddAssignedExam(int examId, AssignedExam assignedExam)
        {
            assignedExam.ExamId = examId;
            _context.AssignedExams.Add(assignedExam);
            _context.SaveChanges();
        }
    }
}
