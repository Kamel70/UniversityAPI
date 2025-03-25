using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityAPI.DTO;
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
            List<Student> students = IStudentrRepository.GetAll();
            if (students.Count == 0)
            {
                return NotFound();
            }
            return Ok(students);
        }
        [HttpGet("stdWithdept")]
        public IActionResult GetStudentsWithDept()
        {
            List<Student> students = IStudentrRepository.GetStudentsWithDept();
            if (students.Count == 0)
            {
                return NotFound();
            }
            
            List<StudentWithDeptNameDTO> data = new List<StudentWithDeptNameDTO>();
            foreach (var std in students)
            {
                StudentWithDeptNameDTO studentWithDeptNameDTO = new StudentWithDeptNameDTO();
                studentWithDeptNameDTO.Name = std.Name;
                studentWithDeptNameDTO.DepartmentName = std.Department.Name;
                studentWithDeptNameDTO.Age = std.Age;
                studentWithDeptNameDTO.Address = std.Address;
                data.Add(studentWithDeptNameDTO);
            }
            return Ok(data);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Student studentbyID = IStudentrRepository.GetById(id);
            if (studentbyID == null)
            {
                return NotFound();
            }
            return Ok(new {message=$"Student With ID:{id} is Exist",student=studentbyID});
        }

        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            Student studentbyName= IStudentrRepository.GetByName(name);
            if (studentbyName == null)
            {
                return NotFound();
            }
            return Ok(new { message = $"{name} is Exist", student = studentbyName });
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            IStudentrRepository.Delete(id);
            IStudentrRepository.Save();
            return Ok("deleted Successfully");
        }
        [HttpPost]
        public IActionResult Add(Student student)
        {
            if (student == null) return BadRequest();
            IStudentrRepository.Add(student);
            IStudentrRepository.Save();
            return CreatedAtAction(nameof(GetById),new {id=student.Id},new {message="Created Successfully"});
        }
        [HttpPut]
        public IActionResult Update(Student student)
        {
            if(student == null) return BadRequest();
            IStudentrRepository.Update(student);
            IStudentrRepository.Save();
            return Ok("Updated Successfully");
        }

    }
}
