using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProjectData.Entities
{
    public class Question
    {
        public Question()
        {
            this.Exams = new HashSet<Exam>();
        }
        public int QuestionId { get; set; }
        [Required]
        [MaxLength(256)]
        public string Text { get; set; }
        [Required]
        public int Type { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }
        public ICollection<QuestionOption> QuestionOption { get; set; }
        public ICollection<StudentAnswer> StudentAnswer { get; set; }

    }
}
