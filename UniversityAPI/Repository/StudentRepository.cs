using Microsoft.EntityFrameworkCore;
using UniversityAPI.Models;

namespace UniversityAPI.Repository
{
    public class StudentRepository : IStudentrRepository
    {
        UniversityContext universityContext;
        public StudentRepository(UniversityContext _universityContext) 
        {
            universityContext = _universityContext;    
        }
        public void Add(Student student)
        {
            universityContext.Students.Add(student);
        }

        public void Delete(int id)
        {
            Student student = GetById(id);
            universityContext.Students.Remove(student);
        }

        public List<Student> GetAll()
        {
            return universityContext.Students.ToList();
        }

        public Student GetById(int id)
        {
            return universityContext.Students.FirstOrDefault(s=>s.Id==id);
        }

        public Student GetByName(string name)
        {
            return universityContext.Students.FirstOrDefault(s => s.Name == name);
        }

        public void Update(Student student)
        {
            universityContext.Students.Update(student);
        }

        public void Save()
        {
            universityContext.SaveChanges();
        }

        public List<Student> GetStudentsWithDept()
        {
            return universityContext.Students.Include(s=>s.Department).ToList();
        }
    }
}
