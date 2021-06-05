using System;
using System.Net.Http;
using System.Threading.Tasks;
using Patterns.Mediator.ConsoleApp.DTO;
using Patterns.Mediator.ConsoleApp.Messages;

namespace Patterns.Mediator.ConsoleApp.Services
{
    public class ExternalCustomerSearchService : IExternalCustomerSearchService
    {
        private readonly HttpClient _client;

        public ExternalCustomerSearchService(HttpClient client)
        {
            _client = client;
        }

        public async Task<CustomerDto> SearchAsync(string email)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            //throw new CustomerSearchException("EXT_CUSTOMER_SEARCH_BY_EMAIL", "error occurred when searching for the customer");

            return new CustomerDto
            {
                FirstName = "Cheranga",
                LastName = "Hatangala",
                DateOfBirth = new DateTime(1982, 11, 1)
            };
        }
    }
}