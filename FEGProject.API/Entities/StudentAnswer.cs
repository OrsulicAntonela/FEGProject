using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProject.API.Entities
{
    public class StudentAnswer
    {
        public int StudentAnswerId { get; set; }
        public string Answer { get; set; }

        public int StudentAssignedExamId { get; set; }
        public StudentAssignedExam StudentAssignedExam { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

    }
}
