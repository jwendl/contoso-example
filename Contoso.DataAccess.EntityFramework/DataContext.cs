using Contoso.DataAccess.EntityFramework.Interfaces;
using Contoso.DataAccess.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace Contoso.DataAccess.EntityFramework
{
    public class DataContext
        : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public DbSet<Customer> Customers { get; set; }
    }
}
