using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FEGProjectData.Entities
{
    public class Exam
    {
        public int ExamId { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        
        public ICollection<Question> Questions { get; set; }
        public ICollection<AssignedExam> AssignedExams { get; set; }

    }
}
