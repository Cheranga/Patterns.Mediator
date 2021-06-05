using FluentValidation;
using Patterns.Mediator.ConsoleApp.DTO;

namespace Patterns.Mediator.ConsoleApp.Validators
{
    public class GetCustomerByEmailRequestValidator : ModelValidatorBase<GetCustomerByEmailRequest>
    {
        public GetCustomerByEmailRequestValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty();
        }
    }
}