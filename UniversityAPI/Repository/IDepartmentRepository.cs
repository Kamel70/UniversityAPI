using UniversityAPI.Models;

namespace UniversityAPI.Repository
{
    public interface IDepartmentRepository
    {
        public List<Department> GetAll();
        public Department GetById(int id);
        public Department GetByName(string name);

        public void Add(Department dept);
        public void Update(Department dept);
        public void Delete(int id);
        public void Save();
        public List<Department> GetAllWithStudents();
    }
}
