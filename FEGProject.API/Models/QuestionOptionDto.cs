using System.ComponentModel.DataAnnotations;

namespace FEGProject.API.Models
{
    public class QuestionOptionDto
    {
        public int QuestionOptionId { get; set; }
        [Required]
        public int Option { get; set; }
        [Required]
        [MaxLength(256)]
        public string Text { get; set; }
        public int QuestionId { get; set; }
    }
}
