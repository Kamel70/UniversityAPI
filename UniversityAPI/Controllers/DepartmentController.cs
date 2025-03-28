﻿using Microsoft.AspNetCore.Http;
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
        public IDepartmentRepository departmentRepository { get; }
        public DepartmentController(IDepartmentRepository _departmentRepository)
        {
            departmentRepository = _departmentRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Department> departments = departmentRepository.GetAll();
            if (departments.Count == 0)
            {
                return NotFound();
            }
            return Ok(departments);
        }
        [HttpGet("ds")]
        [TypeFilter(typeof(ResponseHeaderFilter))]
        public IActionResult GetAllWithStudents()
        {
            List<Department> departments = departmentRepository.GetAllWithStudents();
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
            departmentRepository.Add(newDept);
            departmentRepository.Save();
            return Ok("Department Added Successfully");
        }

        [HttpPost()]
        public IActionResult Add(string name,string loc)
        {
            Department newDept = new Department();
            newDept.Name = name;
            newDept.Location = loc;
            departmentRepository.Add(newDept);
            departmentRepository.Save();
            return Ok("Department Added Successfully");
        }


    }
}
