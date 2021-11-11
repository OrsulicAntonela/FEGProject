using FEGProjectData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProjectData.Repository
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAll();
        Student GetStudent(int studentId);

    }
}
