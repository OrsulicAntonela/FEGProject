using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FEGProjectData.Entities
{
    public class AssignedExam
    {
        public AssignedExam()
        {
            this.Groups = new HashSet<Group>();
        }
        public int AssignedExamId { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        [MaxLength(256)]
        public string Password { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public ICollection<StudentAssignedExam> StudentAssignedExams { get; set; }
        public ICollection<Group> Groups { get; set; }

    }
}
