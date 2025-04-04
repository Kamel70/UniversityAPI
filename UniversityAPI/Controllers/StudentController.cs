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
        IBaseRepository<Student> baseRepository;
        public StudentController(IBaseRepository<Student> _baseRepository)
        {
            baseRepository = _baseRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Student> students = baseRepository.GetAll();
            if (students.Count == 0)
            {
                return NotFound();
            }
            return Ok(students);
        }
        [HttpGet("stdWithdept")]
        public IActionResult GetStudentsWithDept()
        {
            List<Student> students = baseRepository.GetStudentsWithDept("Department");
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
            Student studentbyID = baseRepository.GetBy(s=>s.Id==id);
            if (studentbyID == null)
            {
                return NotFound();
            }
            return Ok(new {message=$"Student With ID:{id} is Exist",student=studentbyID});
        }

        [HttpGet("{name:alpha}")]
        public IActionResult GetByName(string name)
        {
            Student studentbyName= baseRepository.GetBy(s=>s.Name==name);
            if (studentbyName == null)
            {
                return NotFound();
            }
            return Ok(new { message = $"{name} is Exist", student = studentbyName });
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            baseRepository.Delete(s => s.Id == id);
            baseRepository.Save();
            return Ok("deleted Successfully");
        }
        [HttpPost]
        public IActionResult Add(Student student)
        {
            if (student == null) return BadRequest();
            baseRepository.Add(student);
            baseRepository.Save();
            return CreatedAtAction(nameof(GetById),new {id=student.Id},new {message="Created Successfully"});
        }
        [HttpPut]
        public IActionResult Update(Student student)
        {
            if(student == null) return BadRequest();
            baseRepository.Update(student);
            baseRepository.Save();
            return Ok("Updated Successfully");
        }

    }
}
