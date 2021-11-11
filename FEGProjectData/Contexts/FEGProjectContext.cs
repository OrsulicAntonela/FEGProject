using FEGProjectData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProjectData.Contexts
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


        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FEGProjectContext>
        {
            public FEGProjectContext CreateDbContext(string[] args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../FEGProject.API/appsettings.json").Build();
                var builder = new DbContextOptionsBuilder<FEGProjectContext>();
                var connectionString = configuration.GetConnectionString("DatabaseConnection");
                builder.UseSqlServer(connectionString);
                return new FEGProjectContext(builder.Options);
            }
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        
        //}
    }
}
