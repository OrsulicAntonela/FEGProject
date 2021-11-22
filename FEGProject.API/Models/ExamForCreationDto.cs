using FEGProjectData.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProject.API.Models
{
    public class ExamForCreationDto
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
    }
}
