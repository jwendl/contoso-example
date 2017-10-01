using ContosoExample.Business.Interfaces;
using ContosoExample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ContosoExample.Controllers
{
    public class HomeController
        : Controller
    {
        readonly IOrderDashboardService orderDashboardService;

        public HomeController(IOrderDashboardService orderDashboardService)
        {
            this.orderDashboardService = orderDashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var orderDashboard = await orderDashboardService.FetchOrderDashboardAsync();
            return View(orderDashboard);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
