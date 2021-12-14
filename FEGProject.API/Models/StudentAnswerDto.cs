using System.ComponentModel.DataAnnotations;

namespace FEGProject.API.Models
{
    public class StudentAnswerDto
    {
        public int StudentAnswerId { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Answer { get; set; }
        public int StudentAssignedExamId { get; set; }
        public int? QuestionId { get; set; }
    }
}
