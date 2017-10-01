using ContosoExample.Business.Interfaces;
using ContosoExample.Business.Models;
using ContosoExample.Data.Interfaces;
using ContosoExample.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoExample.Business.Services
{
    public class OrderDashboardService
        : IOrderDashboardService
    {
        readonly IDataRepository<Order> orderRepository;

        public OrderDashboardService(IDataRepository<Order> orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<OrderDashboard> FetchOrderDashboardAsync()
        {
            var orders = await orderRepository.FetchAllAsync(o => o.Customer, o => o.Customer.Location);

            var orderDashboard = new OrderDashboard
            {
                OrderDashboardItems = orders
                    .Select(o => new OrderDashboardItem()
                    {
                        Id = o.Id,
                        OrderNumber = o.OrderNumber,
                        State = o.Customer.Location.State,
                    })
            };

            return orderDashboard;
        }
    }
}
