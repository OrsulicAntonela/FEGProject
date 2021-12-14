using System;
using System.Collections.Generic;

namespace FEGProject.API.Models
{
    public class StudentAssignedExamDto
    {
        public int StudentAssignedExamId { get; set; }
        public DateTime DeliveryTime { get; set; }
        public double? Result { get; set; }
        
        public int StudentId { get; set; }
        public StudentDto Student { get; set; }
       
        public int AssignedExamId { get; set; }
        public AssignedExamDto AssignedExam { get; set; }

        public ICollection<StudentAnswerDto> StudentAnswers { get; set; }
    }
}
