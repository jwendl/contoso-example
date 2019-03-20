using AutoMapper;
using Contoso.BusinessLogic.Models;
using Contoso.DataAccess.Cosmos.Models;

namespace Contoso.BusinessLogic.Bootstrappers.Profiles
{
    public class CustomerProfile
        : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerItem>();
            CreateMap<CustomerItem, Customer>();
        }
    }
}
