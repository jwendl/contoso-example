using Contoso.DataAccess.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Contoso.DataAccess.EntityFramework.Interfaces
{
    public interface IDataContext
    {
        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        DbSet<Customer> Customers { get; set; }
    }
}
