using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UniversityAPI.Models;

namespace UniversityAPI.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        UniversityContext universityContext;
        public BaseRepository(UniversityContext _universityContext)
        {
            universityContext = _universityContext;
        }
        public void Add(T obj)
        {
            universityContext.Set<T>().Add(obj);
        }

        public void Delete(Expression<Func<T, bool>> fun)
        {
            T obj = GetBy(fun);
            universityContext.Set<T>().Remove(obj);
        }

        public List<T> GetAll()
        {
            return universityContext.Set<T>().ToList();
        }

        public T GetBy(Expression<Func<T, bool>> fun)
        {
            return universityContext.Set<T>().FirstOrDefault(fun);
        }

        public List<T> GetDeptsWithStudents(string studs)
        {
            return universityContext.Set<T>().Include(studs).ToList();
        }

        public T GetStudentDetailsWithDept(string dept, Expression<Func<T, bool>> fun)
        {
            return universityContext.Set<T>().Include(dept).FirstOrDefault(fun);
        }

        public List<T> GetStudentsWithDept(string depts)
        {
            return universityContext.Set<T>().Include(depts).ToList();
        }

        public void Save()
        {
            universityContext.SaveChanges();
        }

        public void Update(T obj)
        {
            universityContext.Set<T>().Update(obj);
        }
    }
}
