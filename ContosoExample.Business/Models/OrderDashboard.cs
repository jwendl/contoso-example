using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ContosoExample.Business.Models
{
    public class OrderDashboard
    {
        public OrderDashboard()
        {
            OrderDashboardItems = new Collection<OrderDashboardItem>();
        }

        public IEnumerable<OrderDashboardItem> OrderDashboardItems { get; set; }
    }
}
