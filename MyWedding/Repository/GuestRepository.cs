using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWedding.Models;
using MyWedding.Repository;


namespace MyWedding.Repository
{
    public class GuestRepository<T> : IGuestRepository<T> where T : Guest
    {
        private readonly AppDbContext _DBcontext;
        private readonly DbSet<T> _dbset;

        public GuestRepository(AppDbContext DBcontext)
        {
            _DBcontext = DBcontext;
            _dbset = DBcontext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbset.Add(entity);
            _DBcontext.SaveChanges();
        }

        public void Delete(string id)
        {
            var result = _dbset.SingleOrDefault(x => x.id == id);
            _dbset.Remove(result);
            _DBcontext.SaveChanges();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var result = _dbset.Where(x => x.id == id).SingleOrDefaultAsync();
            return await result;
        }

        public async Task<List<T>> ListAsync()
        {
            var result = _dbset.ToListAsync();
            return await result;
        }

        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate)
        {
            var result = _dbset.Where(predicate).ToListAsync();
            return await result;
        }

        public void Update(T entity)
        {
            _dbset.Update(entity);
            _DBcontext.SaveChanges();
        }
    }
}
