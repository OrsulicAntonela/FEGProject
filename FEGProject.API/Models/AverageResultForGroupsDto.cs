using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProject.API.Models
{
    public class AverageResultForGroupDto
    {
        public string GroupName { get; set; }
        public List<AverageResultForExamsDto> AverageResultForExams { get; set; }

    }

    public class AverageResultForExamsDto
    {
        public string ExamName { get; set; }
        public double AverageResult { get; set; }

    }
}
