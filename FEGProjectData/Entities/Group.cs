using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProjectData.Entities
{
    public class Group
    {
        public Group()
        {
            this.AssignedExams = new HashSet<AssignedExam>();
        }
        public int GroupId { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        public ICollection<Student> Student { get; set; }
        public ICollection<AssignedExam> AssignedExams { get; set; }

    }
}
