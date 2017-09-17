using ContosoExample.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ContosoExample.Data.Interfaces
{
    public interface IDataRepository<TEntity>
        where TEntity : BaseModel
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<bool> ExistsAsync(TEntity entity);
        Task<IEnumerable<TEntity>> FetchAllAsync();
        Task<IEnumerable<TEntity>> FetchAllAsync(params Expression<Func<TEntity, bool>>[] includes);
        Task<TEntity> FetchOneAsync(int id);
        Task<TEntity> FetchOneAsync(int id, params Expression<Func<TEntity, bool>>[] includes);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, bool>>[] includes);
        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}