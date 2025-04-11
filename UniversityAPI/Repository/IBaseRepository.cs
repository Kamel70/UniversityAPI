using System.Linq.Expressions;
using UniversityAPI.Models;

namespace UniversityAPI.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        public List<T> GetAll();
        public T GetBy(Expression<Func<T,bool>> fun);
        public void Add(T obj);
        public void Update(T obj);
        public void Delete(Expression<Func<T, bool>> fun);
        public void Save();
        public List<T> GetDeptsWithStudents(string studs);
        public List<T> GetStudentsWithDept(string dept);
        public T GetStudentDetailsWithDept(string dept,Expression<Func<T, bool>> fun);


    }
}
