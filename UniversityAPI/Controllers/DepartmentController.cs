using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityAPI.DTO;
using UniversityAPI.Filters;
using UniversityAPI.Models;
using UniversityAPI.Repository;

namespace UniversityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        public IBaseRepository<Department> baseRepository;
        public DepartmentController(IBaseRepository<Department> baseRepository)
        {
            this.baseRepository = baseRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Department> departments = baseRepository.GetAll();
            if (departments.Count == 0)
            {
                return NotFound();
            }
            return Ok(departments);
        }
        [HttpGet("ds")]
        [TypeFilter(typeof(ResponseHeaderFilter))]
        [Authorize]
        public IActionResult GetAllWithStudents()
        {
            List<Department> departments = baseRepository.GetDeptsWithStudents("Students");
            if (departments.Count == 0)
            {
                return NotFound();
            }
            List<AllDeptsWithStdNamesDTO> data = new List<AllDeptsWithStdNamesDTO>();
            foreach (var dept in departments)
            {
                AllDeptsWithStdNamesDTO allDeptsWithStdNamesDTO = new AllDeptsWithStdNamesDTO();
                allDeptsWithStdNamesDTO.DeptName = dept.Name;
                List<string> stdNames = new List<string>();
                foreach (var std in dept.Students)
                {
                    stdNames.Add(std.Name);
                }
                allDeptsWithStdNamesDTO.StudentNames = stdNames;
                allDeptsWithStdNamesDTO.deptLocatin = dept.Location;
                allDeptsWithStdNamesDTO.StudentCount= stdNames.Count;
                allDeptsWithStdNamesDTO.Message = stdNames.Count>3?"the department has many student":"the department doesn't have enough students";
                data.Add(allDeptsWithStdNamesDTO);
            }
            
            
            return Ok(data);
        }

        [HttpPost("Egypt")]
        public IActionResult AddLocEgypt(string name)
        {
            Department newDept=new Department();
            newDept.Name = name;
            newDept.Location = "Egypt";
            baseRepository.Add(newDept);
            baseRepository.Save();
            return Ok("Department Added Successfully");
        }

        [HttpPost()]
        public IActionResult Add([FromBody] DeptNameAndLocDTO dept)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            Department newDept = new Department();
            newDept.Name = dept.Name;
            newDept.Location = dept.Location;
            baseRepository.Add(newDept);
            baseRepository.Save();
            return Ok("Department Added Successfully");
        }

        [HttpPut]
        public IActionResult Edit(DeptIdAndNameAndLocationDTO department)
        {
            Department dept = new Department();
            dept.Id = department.Id;
            dept.Name = department.Name;
            dept.Location = department.Location;
            baseRepository.Update(dept);
            baseRepository.Save();
            return Ok("Updated Successfully");
        }


    }
}
