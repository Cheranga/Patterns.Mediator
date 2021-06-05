using System.Threading.Tasks;
using FluentValidation;
using Patterns.Mediator.ConsoleApp.Core;
using Patterns.Mediator.ConsoleApp.DTO;

namespace Patterns.Mediator.ConsoleApp.Services
{
    public class CustomerSearchService : ICustomerSearchService
    {
        private readonly IValidator<GetCustomerByEmailRequest> _validator;

        public CustomerSearchService(IValidator<GetCustomerByEmailRequest> validator)
        {
            _validator = validator;
        }
        
        public async Task<Result<GetCustomerResponse>> SearchAsync(GetCustomerByEmailRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return Result<GetCustomerResponse>.Failure("INVALID_REQUEST", validationResult);
            }

            return Result<GetCustomerResponse>.Success(null);
        }
    }
}