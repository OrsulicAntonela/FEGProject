using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEG.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
      
        public StudentController()
        {
        }

        [HttpGet]
        public string Get()
        {
            return "AAA";
        }
    }
}
