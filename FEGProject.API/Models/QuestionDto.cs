using FEGProjectData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProject.API.Models
{
    public class QuestionDto
    {
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int? Type { get; set; }

        public ICollection<QuestionOptionDto> QuestionOption { get; set; }

    }
}
