using System.Threading.Tasks;
using Patterns.Mediator.ConsoleApp.Core;
using Patterns.Mediator.ConsoleApp.DTO;

namespace Patterns.Mediator.ConsoleApp.Services
{
    public interface ICustomerService
    {
        Task<Result<GetCustomerResponse>> SearchAsync(GetCustomerByEmailRequest request);
        Task<Result<GetCustomerResponse>> SearchAsync(GetCustomerByIdRequest request);
        Task CreateCustomerAsync(CreateCustomerRequest request);
        Task<Result<GetCustomerResponse>> UpdateCustomerAsync(UpdateCustomerRequest request);
    }
}