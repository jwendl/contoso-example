using Contoso.DataAccess.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contoso.DataAccess.EntityFramework.Interfaces
{
    public interface IDataRepository<TEntity, TKey>
        where TEntity : BaseModel<TKey>
        where TKey : struct
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task<bool> ExistsAsync(TEntity entity);

        Task<IEnumerable<TEntity>> FetchAllAsync();

        Task<IEnumerable<TEntity>> FetchAllAsync(params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> FetchOneAsync(TKey id);

        Task<TEntity> FetchOneAsync(TKey id, params Expression<Func<TEntity, object>>[] includes);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}
