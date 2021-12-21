using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace Framework.Infrastructure
{
    public class BaseRepo<T> : IRepo<T> where T : class
    {

        private readonly DbContext _context;
        public DbSet<T> Entities { get; }
        public BaseRepo(DbContext context)
        {
            _context = context;

            Entities = _context.Set<T>();
        }

        public IQueryable<T> Get => Entities;
        public IQueryable<T> GetNoTraking => Entities.AsNoTracking();
        public async Task CreateAsync(T entity)
        {
            await Entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            Entities.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            Entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetById(params object[] Ids)
        {
            return await Entities.FindAsync(Ids);
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
