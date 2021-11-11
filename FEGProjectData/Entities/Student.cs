using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProjectData.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Surname { get; set; }
        [Required]
        [MaxLength(100)]
        public string Adderss { get; set; }
        [Required]
        [MaxLength(100)]
        public string EmailAddress { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public ICollection<StudentAssignedExam> StudentAssignedExam { get; set; }

    }
}
