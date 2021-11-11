using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProjectData.Entities
{
    public class Exam
    {
        public Exam()
        {
            this.Questions = new HashSet<Question>();
        }
        public int ExamId { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<AssignedExam> AssignedExams { get; set; }

    }
}
