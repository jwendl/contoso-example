using Contoso.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Contoso.Web.Controllers
{
    public class CustomerController
        : Controller
    {
        private readonly ICustomerListService customerListService;

        public CustomerController(ICustomerListService customerListService)
        {
            this.customerListService = customerListService;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await customerListService.FetchAllCustomersAsync();
            return View(customers);
        }
    }
}
