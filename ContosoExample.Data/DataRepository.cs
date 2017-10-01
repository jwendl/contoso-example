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
        private IDataContext DataContext { get; set; }

        public DataRepository(IDataContext dataContext)
        {
            this.DataContext = dataContext;
        }

        public async Task<IEnumerable<TEntity>> FetchAllAsync() => await DataContext.Set<TEntity>()
            .ToListAsync();

        public async Task<IEnumerable<TEntity>> FetchAllAsync(params Expression<Func<TEntity, BaseModel>>[] includes)
        {
            var query = DataContext.Set<TEntity>();
            includes.ToList().ForEach((include) => query.Include(include));
            return await query.ToListAsync();
        }

        public async Task<TEntity> FetchOneAsync(int id) => await DataContext.Set<TEntity>()
            .FindAsync(id);

        public async Task<TEntity> FetchOneAsync(int id, params Expression<Func<TEntity, BaseModel>>[] includes)
        {
            var query = DataContext.Set<TEntity>();
            includes.ToList().ForEach((include) => query.Include(include));
            return await query.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate) => await DataContext.Set<TEntity>()
            .Where(predicate).ToListAsync();

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, BaseModel>>[] includes)
        {
            var query = DataContext.Set<TEntity>();
            includes.ToList().ForEach((include) => query.Include(include));
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate) => await DataContext.Set<TEntity>()
            .Where(predicate).SingleOrDefaultAsync();

        public async Task<bool> ExistsAsync(TEntity entity) => await DataContext.Set<TEntity>()
            .Where(e => e.Id == entity.Id).AnyAsync();

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await DataContext.Set<TEntity>().AddAsync(entity);
            var validationContext = new ValidationContext(entity);
            Validator.ValidateObject(entity, validationContext, true);
            await DataContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            DataContext.Set<TEntity>().Update(entity);
            var validationContext = new ValidationContext(entity);
            Validator.ValidateObject(entity, validationContext, true);
            await DataContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            DataContext.Set<TEntity>().Remove(entity);
            await DataContext.SaveChangesAsync();
        }
    }
}
