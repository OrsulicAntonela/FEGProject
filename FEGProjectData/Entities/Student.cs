using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FEGProjectData.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Adderss { get; set; }
        [MaxLength(100)]
        public string EmailAddress { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public ICollection<StudentAssignedExam> StudentAssignedExams { get; set; }

    }
}
