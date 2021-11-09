using FEGProject.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProject.API.Contexts
{
    public class FEGProjectContext : DbContext
    {
        public DbSet<AssignedExam> AssignedExams { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        public DbSet<StudentAssignedExam> StudentAssignedExams { get; set; }

        public FEGProjectContext(DbContextOptions<FEGProjectContext> options)
            : base(options)
        {

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
