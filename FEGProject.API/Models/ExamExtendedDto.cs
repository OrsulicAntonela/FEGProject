using System;
using System.Collections.Generic;

namespace FEGProject.API.Models
{
    public class ExamExtendedDto: ExamDto
    {
        public new ICollection<QuestionExtendedDto> Questions { get; set; }
        public DateTime Deadline { get; set; }
    }
}
