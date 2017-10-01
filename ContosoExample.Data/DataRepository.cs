using ContosoExample.Data.Extensions;
using ContosoExample.Data.Interfaces;
using ContosoExample.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ContosoExample.Data
{
    public class DataRepository<TEntity>
        : IDataRepository<TEntity>
        where TEntity : BaseModel
    {
        readonly IDataContext dataContext;

        public DataRepository(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<IEnumerable<TEntity>> FetchAllAsync() => await dataContext.Set<TEntity>()
            .ToListAsync();

        public async Task<IEnumerable<TEntity>> FetchAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = dataContext.Set<TEntity>().BuildIncludes(includes);
            return await query.ToListAsync();
        }

        public async Task<TEntity> FetchOneAsync(int id) => await dataContext.Set<TEntity>()
            .FindAsync(id);

        public async Task<TEntity> FetchOneAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = dataContext.Set<TEntity>().BuildIncludes(includes);
            return await query.Where(e => e.Id == id).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate) => await dataContext.Set<TEntity>()
            .Where(predicate).ToListAsync();

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = dataContext.Set<TEntity>().BuildIncludes(includes);
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate) => await dataContext.Set<TEntity>()
            .Where(predicate).SingleOrDefaultAsync();

        public async Task<bool> ExistsAsync(TEntity entity) => await dataContext.Set<TEntity>()
            .Where(e => e.Id == entity.Id).AnyAsync();

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await dataContext.Set<TEntity>().AddAsync(entity);
            var validationContext = new ValidationContext(entity);
            Validator.ValidateObject(entity, validationContext, true);
            await dataContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            dataContext.Set<TEntity>().Update(entity);
            var validationContext = new ValidationContext(entity);
            Validator.ValidateObject(entity, validationContext, true);
            await dataContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            dataContext.Set<TEntity>().Remove(entity);
            await dataContext.SaveChangesAsync();
        }
    }
}
