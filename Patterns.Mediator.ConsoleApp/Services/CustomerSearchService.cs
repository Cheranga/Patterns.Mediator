using System.Threading.Tasks;
using FluentValidation;

namespace Patterns.Mediator.ConsoleApp
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