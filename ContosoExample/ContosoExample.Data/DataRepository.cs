using ContosoExample.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ContosoExample.Data
{
    public class DataRepository<TEntity>
        : IDataRepository<TEntity>
        where TEntity : class
    {
        private IDataContext dataContext { get; set; }

        public DataRepository(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<IEnumerable<TEntity>> FetchAllAsync() => await dataContext.Set<TEntity>()
            .ToListAsync();

        public async Task<IEnumerable<TEntity>> FetchAllAsync(params Expression<Func<TEntity, bool>>[] includes)
        {
            var query = dataContext.Set<TEntity>();
            includes.ToList().ForEach((include) => query.Include(include));
            return await query.ToListAsync();
        }

        public async Task<TEntity> FetchOneAsync(int id) => await dataContext.Set<TEntity>()
            .FindAsync(id);

        public async Task<TEntity> FetchOneAsync(int id, params Expression<Func<TEntity, bool>>[] includes)
        {
            var query = dataContext.Set<TEntity>();
            includes.ToList().ForEach((include) => query.Include(include));
            return await query.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate) => await dataContext.Set<TEntity>()
            .Where(predicate).ToListAsync();

        public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate) => await dataContext.Set<TEntity>()
            .Where(predicate).SingleOrDefaultAsync();
    }
}
