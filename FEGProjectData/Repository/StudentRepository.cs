using FEGProjectData.Contexts;
using FEGProjectData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProjectData.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly FEGProjectContext _context;

        public StudentRepository(FEGProjectContext context)
        {
            _context = context;
        }
        public IEnumerable<Student> GetAll()
        {
            return _context.Students.OrderBy(s => s.Surname).ThenBy(s => s.Name).ToList();
        }

        public Student GetStudent(int studentId)
        {
            return _context.Students.Where(s => s.StudentId == studentId).FirstOrDefault();
        }
    }
}
