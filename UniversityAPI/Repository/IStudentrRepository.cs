using UniversityAPI.Models;

namespace UniversityAPI.Repository
{
    public interface IStudentrRepository
    {
        public List<Student> GetAll();
        public Student GetById(int id);
        public Student GetByName(string name);

        public void Add(Student student);
        public void Update(Student student);
        public void Delete(int id);
        public void Save();

    }
}
