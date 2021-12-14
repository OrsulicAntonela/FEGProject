using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public ICollection<Student> Students { get; set; }
        public ICollection<AssignedExam> AssignedExams { get; set; }

    }
}
