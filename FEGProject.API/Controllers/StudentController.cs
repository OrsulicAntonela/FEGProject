using AutoMapper;
using FEGProject.API.Models;
using FEGProjectData.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FEGProject.API.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class StudentController: ControllerBase
    {
        private readonly StudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentController(StudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _studentRepository.GetAll();
            return Ok(_mapper.Map<IEnumerable<StudentDto>>(students));
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            var student = _studentRepository.GetStudent(id);
            
            if(student == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<StudentDto>(student));

        }
    }
}
