using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProject.API.Entities
{
    public class AssignedExam
    {
        public AssignedExam()
        {
            this.Groups = new HashSet<Group>();
        }
        public int AssignedExamId { get; set; }
        public DateTime Deadline { get; set; }
        public int Password { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public ICollection<StudentAssignedExam> StudentAssignedExam { get; set; }
        public virtual ICollection<Group> Groups { get; set; }

    }
}
