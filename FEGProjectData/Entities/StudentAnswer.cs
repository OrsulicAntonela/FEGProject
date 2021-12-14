using System.ComponentModel.DataAnnotations;

namespace FEGProjectData.Entities
{
    public class StudentAnswer
    {
        public int StudentAnswerId { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Answer { get; set; }

        public int StudentAssignedExamId { get; set; }
        public StudentAssignedExam StudentAssignedExam { get; set; }

        public int? QuestionId { get; set; }
        public Question Question { get; set; }

    }
}
