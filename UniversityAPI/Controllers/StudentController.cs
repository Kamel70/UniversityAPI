using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
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
        UserManager<ApplicationUser> userManager;
        public StudentController(IBaseRepository<Student> _baseRepository, UserManager<ApplicationUser> userManager)
        {
            baseRepository = _baseRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles ="HR")]
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
        [Authorize(Roles ="HR")]
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
        [Authorize(Roles ="HR")]
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
        [Authorize(Roles = "HR")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            Student std = baseRepository.GetBy(s => s.Id == id);
            if (std != null)
            {
                baseRepository.Delete(s => s.Id == id);
                baseRepository.Save();
                return Ok("Deleted Successfully");
            }
            return BadRequest("Not Found");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(StudentDetailsAndUserDetailsDTO std)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.Email = std.Email;
                user.UserName = std.UserName;
                user.PhoneNumber = std.PhoneNumber;
                user.Image = std.Image;
                user.Age = std.Age;
                user.Address = std.Address;
                IdentityResult id = await userManager.CreateAsync(user,std.Password);
                if (id.Succeeded)
                {
                    IdentityResult role = await userManager.AddToRoleAsync(user, "Student");
                    if (role.Succeeded)
                    {
                        Student student = new Student();
                        student.Name = std.Name;
                        student.Address = std.Address;
                        student.Image = std.Image;
                        student.PhoneNumber = std.PhoneNumber;
                        student.Age = std.Age;
                        student.BOD = std.BOD;
                        student.DeptId = std.DeptID;
                        student.UserID = user.Id;
                        baseRepository.Add(student);
                        baseRepository.Save();
                        return CreatedAtAction(nameof(GetById), new { id = student.Id }, new { message = "Created Successfully" });
                    }
                }
                foreach (var item in id.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        [Authorize(Roles = "HR")]
        public IActionResult Update(Student student)
        {
            if(student == null) return BadRequest();
            baseRepository.Update(student);
            baseRepository.Save();
            return Ok("Updated Successfully");
        }

    }
}
