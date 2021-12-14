using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FEGProject.API.Models
{
    public class ExamDto
    {
        public int ExamId { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        public ICollection<QuestionDto> Questions { get; set; }
        //public ICollection<AssignedExamDto> AssignedExams { get; set; }
    }
}
