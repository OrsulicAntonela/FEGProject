using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProject.API.Models
{
    public class StudentDto
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adderss { get; set; }
        public string EmailAddress { get; set; }
        public int GroupId { get; set; }
    }
}
