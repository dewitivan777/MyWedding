using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyWedding.Models;

namespace MyWedding.Repository
{
    public interface IGuestRepository<T>
    {
        Task<T> GetByIdAsync(string id);
        Task<List<string>> GetEmail();
        Task<List<string>> GetEmail(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> ListAsync();

        void Add(T entity);
        void Update(T entity);
        void Delete(string id);
    }
}
