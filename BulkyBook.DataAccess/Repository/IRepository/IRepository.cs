using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T-Category
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties=null);
        IEnumerable<T> GetAll(string? includeProperties=null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity); //Remove multiple items using IEnumerable<T> which is a collection of entities
    }
}
