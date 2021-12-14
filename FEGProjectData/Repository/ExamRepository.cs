using FEGProjectData.Contexts;
using FEGProjectData.Entities;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace FEGProjectData.Repository
{
    public class ExamRepository
    {
        private readonly FEGProjectContext _context;
        private readonly ILogger<ExamRepository> _logger;
        public ExamRepository(FEGProjectContext context ,ILogger<ExamRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void AddExam(Exam exam)
        {
            _context.Exams.Add(exam);
            _context.SaveChanges();
        }

        public Exam GetExamWithQuestions(int examId)
        {
            _logger.LogWarning("GetExamWithQuestions logger.");
            return _context.Exams
                .Include(q => q.Questions)
                .ThenInclude(qo => qo.QuestionOptions)
                .Where(e => e.ExamId == examId)
                .FirstOrDefault();
        }

        public void DeleteExam(int examId)
        {
            var examEntity = _context.Exams.Where(e => e.ExamId == examId).FirstOrDefault();
            _context.Remove(examEntity);
            _context.SaveChanges();
        }

        public void AddAssignedExam(int examId, AssignedExam assignedExam)
        {
            assignedExam.ExamId = examId;
            _context.AssignedExams.Add(assignedExam);
            _context.SaveChanges();
        }

        public AssignedExam GetAssignedExam(int assignedExamId)
        {
            return _context.AssignedExams.Include(g => g.Groups)
                .Where(a => a.AssignedExamId == assignedExamId)
                .FirstOrDefault();
        }
        public List<AssignedExam> GetActiveAssignedExams()
        {
            return _context.AssignedExams.Include(e => e.Exam)
                .Where(a => a.Deadline > DateTime.Now)
                .ToList();
        }
        public void DeleteAssignedExam(int assignedExamId)
        {
            var assignedExamEntity = _context.AssignedExams.Where(a => a.AssignedExamId == assignedExamId).FirstOrDefault();
            _context.Remove(assignedExamEntity);
            _context.SaveChanges();
        }
        public void AddStudentAssignedExam(StudentAssignedExam studentAssignedExam)
        {
            _context.StudentAssignedExams.Add(studentAssignedExam);
            _context.SaveChanges();
        }

        public StudentAssignedExam GetStudentAssignedExam(int examId, int studentId)
        {
            var studentAssignedExam = _context.StudentAssignedExams
                .Include(a => a.AssignedExam)
                .ThenInclude(e => e.Exam)
                .ThenInclude(q => q.Questions)
                .ThenInclude(qo => qo.QuestionOptions)
                .Where(s => s.AssignedExam.ExamId == examId && s.StudentId == studentId)
                .FirstOrDefault();

            return studentAssignedExam;
        }

        public List<StudentAssignedExam> GetListOfStudentAssignedExam(int examId)
        {
            var studentAssignedExam = _context.StudentAssignedExams
                .Include(s => s.Student)
                .Include(a => a.AssignedExam)
                .ThenInclude(e => e.Exam)
                .Where(s => s.AssignedExam.ExamId == examId)
                .ToList();

            return studentAssignedExam;
        }
        public StudentAssignedExam GetStudentAssignedExamToEditResult(int examId, int studentId)
        {
            var studentAssignedExam = _context.StudentAssignedExams
                .Include(s => s.StudentAnswers)
                .Include(a => a.AssignedExam)
                .Where(s => s.AssignedExam.ExamId == examId && s.StudentId == studentId)
                .FirstOrDefault();

            return studentAssignedExam;
        }
        public void AddStudentAnswer( StudentAnswer studentAnswer)
        {
            _context.StudentAnswers.Add(studentAnswer);
            _context.SaveChanges();
        }

        public List<StudentAnswer> GetStudentAnswers(int studntAssignedExamId)
        {
            return _context.StudentAnswers
                .Include(s => s.Question)
                .ThenInclude(q => q.QuestionOptions)
                .Where(s => s.StudentAssignedExamId == studntAssignedExamId)
                .ToList();
        }
        public void DeleteStudentAnswer(int studentId, int questionId)
        {
            var studentAnswer = _context.StudentAnswers
                .Where(s => s.StudentAssignedExam.StudentId == studentId && s.QuestionId == questionId)
                .FirstOrDefault();

            _context.Remove(studentAnswer);
            _context.SaveChanges();
        }
        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
