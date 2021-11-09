using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProject.API.Entities
{
    public class Group
    {
        public Group()
        {
            this.AssignedExams = new HashSet<AssignedExam>();
        }
        public int GroupId { get; set; }
        public string Name { get; set; }

        public ICollection<Student> Ştudent { get; set; }
        public virtual ICollection<AssignedExam> AssignedExams { get; set; }

    }
}
