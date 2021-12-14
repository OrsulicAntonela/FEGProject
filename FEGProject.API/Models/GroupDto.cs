using System.ComponentModel.DataAnnotations;

namespace FEGProject.API.Models
{
    public class GroupDto
    {
        public int GroupId { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        //public ICollection<StudentDto> Students { get; set; }
        //public ICollection<AssignedExamDto> AssignedExams { get; set; }
    }
}
