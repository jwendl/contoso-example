using ContosoExample.Business.Models;
using System.Threading.Tasks;

namespace ContosoExample.Business.Interfaces
{
    public interface IOrderDashboardService
    {
        Task<OrderDashboard> FetchOrderDashboardAsync();
    }
}
