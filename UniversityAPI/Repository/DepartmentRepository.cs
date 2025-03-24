using Microsoft.EntityFrameworkCore;
using UniversityAPI.Models;

namespace UniversityAPI.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        UniversityContext universityContext;
        public DepartmentRepository(UniversityContext _universityContext)
        {
            universityContext = _universityContext;
        }
        public void Add(Department dept)
        {
            universityContext.Departments.Add(dept);
        }

        public void Delete(int id)
        {
            Department dept = GetById(id);
            universityContext.Departments.Remove(dept);
        }

        public List<Department> GetAll()
        {
            return universityContext.Departments.ToList();
        }

        public Department GetById(int id)
        {
            return universityContext.Departments.FirstOrDefault(s => s.Id == id);
        }

        public Department GetByName(string name)
        {
            return universityContext.Departments.FirstOrDefault(s => s.Name == name);
        }

        public void Update(Department dept)
        {
            universityContext.Departments.Update(dept);
        }

        public void Save()
        {
            universityContext.SaveChanges();
        }

        public List<Department> GetAllWithStudents()
        {
            return universityContext.Departments.Include(d=>d.Students).ToList();
        }
    }
}
