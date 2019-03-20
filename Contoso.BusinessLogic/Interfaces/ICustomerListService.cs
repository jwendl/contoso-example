using Contoso.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contoso.BusinessLogic.Interfaces
{
    public interface ICustomerListService
    {
        Task<IEnumerable<CustomerItem>> FetchAllCustomersAsync();
    }
}
