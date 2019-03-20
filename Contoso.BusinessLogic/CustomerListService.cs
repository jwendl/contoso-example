using Contoso.BusinessLogic.Interfaces;
using Contoso.BusinessLogic.Models;
using Contoso.DataAccess.Cosmos.Interfaces;
using Contoso.DataAccess.Cosmos.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.BusinessLogic
{
    public class CustomerListService
        : ICustomerListService
    {
        private readonly IDataRepository<Customer, string> customerRepository;
        private readonly IDateTimeService dateTimeService;

        public CustomerListService(IDataRepository<Customer, string> customerRepository, IDateTimeService dateTimeService)
        {
            this.customerRepository = customerRepository;
            this.dateTimeService = dateTimeService;
        }

        public async Task<IEnumerable<CustomerItem>> FetchAllCustomersAsync()
        {
            var customers = await customerRepository.FetchAllAsync();
            return customers.Select(c => new CustomerItem()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                BirthDate = c.BirthDate,
                Age = dateTimeService.YearsBetween(c.BirthDate, dateTimeService.BuildCurrentDateTime()),
            });
        }
    }
}
