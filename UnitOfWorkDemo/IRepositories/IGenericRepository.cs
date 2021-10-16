using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace UnitOfWorkDemo.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
         Task<IEnumerable<T>> All();
         Task<T> Get(Guid id);
         Task<bool> Add(T entity);
         Task<bool> Delete(Guid Id);
         Task<bool> Upsert(T entity);
         Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    }
}