using FEGProjectData.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProject.API.Models
{
    public class AssignedExamForCreationDto
    {
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        [MaxLength(256)]
        public string Password { get; set; }

        public ICollection<GroupDto> Groups { get; set; }
    }
}
