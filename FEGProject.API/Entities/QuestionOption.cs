using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProject.API.Entities
{
    public class QuestionOption
    {
        public int QuestionOptionId { get; set; }
        public string Text { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
        
    }
}
