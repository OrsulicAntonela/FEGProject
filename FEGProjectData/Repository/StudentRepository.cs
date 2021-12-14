using FEGProjectData.Contexts;
using FEGProjectData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FEGProjectData.Repository
{
    public class StudentRepository 
    {
        private readonly FEGProjectContext _context;

        public StudentRepository(FEGProjectContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public List<Student> GetAll()
        {
            return _context.Students.OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ToList();
        }
        public List<Student> GetByGroupId(int groupId)
        {
            return _context.Students.Where(s => s.GroupId == groupId).OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ToList();
        }

        public Student GetStudent(int studentId)
        {
            return _context.Students.Where(s => s.StudentId == studentId).FirstOrDefault();
        }

        public void AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }
        public List<StudentAssignedExam> GetResultsForStudent(int studentId)
        {
            return _context.StudentAssignedExams
                .Include(s => s.AssignedExam)
                .ThenInclude(s => s.Exam)
                .Where(a => a.StudentId == studentId)
                .ToList();
        }
        public Group GetGroup(int groupId)
        {
            return _context.Groups.Include(s => s.Students).Where(g => g.GroupId == groupId).FirstOrDefault();
        }
        public void AddGroup(Group group)
        {
            _context.Groups.Add(group);
            _context.SaveChanges();
        }

        public List<Group> GetGroups()
        {
            var groups = _context.Groups
                .Include(s => s.Students)
                .ThenInclude(s => s.StudentAssignedExams)
                .ThenInclude(s => s.AssignedExam)
                .ThenInclude(s => s.Exam)
                .ToList();

            return groups;
        }
    }
}
