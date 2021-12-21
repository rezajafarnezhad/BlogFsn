using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Framework.Domain
{
    public interface IRepo<T> where T : class
    {
        DbSet<T> Entities { get; }
        IQueryable<T> Get { get; }
        IQueryable<T> GetNoTraking { get; }
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetById(params object[] Ids);
        Task<int> SaveChangeAsync();
    }
}
