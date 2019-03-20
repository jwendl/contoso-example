using AutoMapper;
using Contoso.BusinessLogic.Interfaces;
using Contoso.BusinessLogic.Models;
using Contoso.DataAccess.Cosmos.Interfaces;
using Contoso.DataAccess.Cosmos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contoso.BusinessLogic
{
    public class CustomerListService
        : ICustomerListService
    {
        private readonly IDataRepository<Customer, string> customerRepository;
        private readonly IMapper mapper;

        public CustomerListService(IDataRepository<Customer, string> customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CustomerItem>> FetchAllCustomersAsync()
        {
            var customers = await customerRepository.FetchAllAsync();
            var customerItems = mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerItem>>(customers);
            return customerItems;
        }
    }
}
