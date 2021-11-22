using FEGProjectData.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProject.API.Models
{
    public class QuestionForCreationDto
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public int? Type { get; set; }

        public ICollection<QuestionOptionDto> QuestionOption { get; set; }

    }
}
