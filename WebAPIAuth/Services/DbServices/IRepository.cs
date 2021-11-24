using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPIAuth.Services
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(long id);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> CreateAsync(T entity);
        T Edit(T entity);
        T Delete(long id);
    }
}
