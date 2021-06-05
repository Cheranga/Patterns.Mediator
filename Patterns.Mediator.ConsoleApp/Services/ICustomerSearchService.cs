using System.Threading.Tasks;

namespace Patterns.Mediator.ConsoleApp
{
    public interface ICustomerSearchService
    {
        Task<Result<GetCustomerResponse>> SearchAsync(GetCustomerByEmailRequest request);
    }
}