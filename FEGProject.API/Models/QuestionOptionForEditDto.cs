using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProject.API.Models
{
    public class QuestionOptionForEditDto
    {
        public int QuestionOptionId { get; set; }
        public int Option { get; set; }
        public string Text { get; set; }
    }
}
