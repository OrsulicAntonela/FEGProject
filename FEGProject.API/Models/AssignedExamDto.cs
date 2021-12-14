using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FEGProject.API.Models
{
    public class AssignedExamDto
    {
        public int AssignedExamId { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        [MaxLength(256)]
        public string Password { get; set; }

        public int ExamId { get; set; }
        public ExamDto Exam { get; set; }

        //public ICollection<StudentAssignedExamDto> StudentAssignedExams { get; set; }
        public ICollection<GroupDto> Groups { get; set; }
    }
}
