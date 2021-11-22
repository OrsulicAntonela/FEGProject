using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProjectData.Entities
{
    public class QuestionOption
    {
        public int QuestionOptionId { get; set; }
        [Required]
        public int Option { get; set; }
        [Required]
        [MaxLength(256)]
        public string Text { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
        
    }
}
