using System.Threading.Tasks;
using Patterns.Mediator.ConsoleApp.Core;
using Patterns.Mediator.ConsoleApp.DTO;

namespace Patterns.Mediator.ConsoleApp.Services
{
    public interface ICustomerSearchService
    {
        Task<Result<GetCustomerResponse>> SearchAsync(GetCustomerByEmailRequest request);
    }
}