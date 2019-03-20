using Contoso.BusinessLogic.Interfaces;
using Contoso.DataAccess.Cosmos.Interfaces;
using Contoso.DataAccess.Cosmos.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.BusinessLogic.UnitTests
{
    [TestClass]
    public class CustomerListServiceTests
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IEnumerable<Customer> expectedCustomers;
        private int callsToDateTimeService = 0;

        public CustomerListServiceTests()
        {
            var serviceCollection = new ServiceCollection();
            var dateTimeService = Substitute.For<IDateTimeService>();
            dateTimeService.WhenForAnyArgs(dts => dts.YearsBetween(Arg.Any<DateTime>(), Arg.Any<DateTime>())).Do(f => callsToDateTimeService++);
            serviceCollection.AddScoped((sp) => dateTimeService);
            serviceCollection.AddScoped((sp) => Substitute.For<ICosmosClientWrapper>(Arg.Any<string>(), Arg.Any<string>()));

            var dataRepository = Substitute.For<IDataRepository<Customer, string>>();
            expectedCustomers = new List<Customer>()
            {
                new Customer() { Id = Guid.NewGuid().ToString(), FirstName = "Test", LastName = "Name", BirthDate = DateTime.UtcNow },
            };
            dataRepository.FetchAllAsync().Returns(expectedCustomers.AsEnumerable());
            serviceCollection.AddScoped((sp) => dataRepository);

            serviceCollection.AddScoped<ICustomerListService, CustomerListService>();
            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [TestMethod]
        public async Task FetchAllCustomersAsync_Succeeds()
        {
            var dataRepository = serviceProvider.GetRequiredService<ICustomerListService>();
            var customers = await dataRepository.FetchAllCustomersAsync();

            customers.Count().Should().Be(1);
            callsToDateTimeService.Should().Be(1);
        }
    }
}
