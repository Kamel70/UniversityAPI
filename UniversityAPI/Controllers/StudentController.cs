using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Models;
using UniversityAPI.Repository;

namespace UniversityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        IStudentrRepository IStudentrRepository;
        public StudentController(IStudentrRepository _IStudentrRepository)
        {
            IStudentrRepository = _IStudentrRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(IStudentrRepository.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(IStudentrRepository.GetById(id));
        }

        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            return Ok(IStudentrRepository.GetByName(name));
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            IStudentrRepository.Delete(id);
            return Ok("deleted Successfully");
        }
        [HttpPost]
        public IActionResult Add(Student student)
        {
            IStudentrRepository.Add(student);
            return Ok("Added Successfully");
        }
        [HttpPut]
        public IActionResult Update(Student student)
        {
            IStudentrRepository.Update(student);
            return Ok("Updated Successfully");
        }

    }
}
