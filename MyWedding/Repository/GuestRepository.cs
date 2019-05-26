using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Linq.Expressions;
using MyWedding.Models;
using MyWedding.Models;
using MyWedding;

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

        public async Task<List<string>> GetEmail()
        {
            var result = _dbset.Select(s => s.email).ToListAsync();
            return await result;
        }

        public async Task<List<string>> GetEmail(Expression<Func<T, bool>> predicate)
        {

            var result = _dbset.Where(predicate).Select(s => s.email).ToListAsync();
            return await result;
        }


        public async Task<IEnumerable<T>> ListAsync()
        {
            var results = _dbset.ToListAsync();

            return await results;
        }

       


        public void Update(T entity)
        {
            _dbset.Update(entity);
            _DBcontext.SaveChanges();
        }


    }
}