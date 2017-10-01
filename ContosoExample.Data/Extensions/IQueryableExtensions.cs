using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ContosoExample.Data.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TEntity> BuildIncludes<TEntity>(this DbSet<TEntity> dbSet, IEnumerable<Expression<Func<TEntity, object>>> includes)
            where TEntity : class
        {
            var query = dbSet.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }
    }
}
