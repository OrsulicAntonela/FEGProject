using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FEGProject.API.Models
{
    public class QuestionDto
    {
        public int QuestionId { get; set; }
        [Required]
        [MaxLength(256)]
        public string Text { get; set; }
        [Required]
        public int Type { get; set; }

        //public int ExamId { get; set; }

        public ICollection<QuestionOptionDto> QuestionOptions { get; set; }
        //public ICollection<StudentAnswerDto> StudentAnswers { get; set; }

    }
}
