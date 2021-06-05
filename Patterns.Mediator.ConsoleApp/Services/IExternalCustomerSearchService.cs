using System.Threading.Tasks;
using Patterns.Mediator.ConsoleApp.DTO;

namespace Patterns.Mediator.ConsoleApp.Services
{
    public interface IExternalCustomerSearchService
    {
        Task<CustomerDto> SearchAsync(string email);
    }
}