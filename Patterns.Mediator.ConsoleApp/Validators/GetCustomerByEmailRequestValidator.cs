using FluentValidation;

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