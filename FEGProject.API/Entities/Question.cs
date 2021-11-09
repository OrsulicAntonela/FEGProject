using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProject.API.Entities
{
    public class Question
    {
        public Question()
        {
            this.Exams = new HashSet<Exam>();
        }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }
        public ICollection<QuestionOption> QuestionOption { get; set; }
        public ICollection<StudentAnswer> StudentAnswer { get; set; }

    }
}
