using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProject.API.Entities
{
    public class StudentAssignedExam
    {
        public int StudentAssignedExamId { get; set; }
        public DateTime DeliveryTime { get; set; }
        public double Result { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int AssignedExamId { get; set; }
        public AssignedExam AssignedExam { get; set; }

        public ICollection<StudentAnswer> StudentAnswer { get; set; }


    }
}
