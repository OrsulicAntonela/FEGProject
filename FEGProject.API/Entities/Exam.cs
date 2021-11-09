using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProject.API.Entities
{
    public class Exam
    {
        public Exam()
        {
            this.Questions = new HashSet<Question>();
        }
        public int ExamId { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<AssignedExam> AssignedExam { get; set; }

    }
}
