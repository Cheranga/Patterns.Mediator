using FluentValidation;
using Patterns.Mediator.ConsoleApp.DTO;

namespace Patterns.Mediator.ConsoleApp.Validators
{
    public class CreateCustomerRequestValidator : ModelValidatorBase<CreateCustomerRequest>
    {
        public CreateCustomerRequestValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.LastName).NotNull().NotEmpty();
            RuleFor(x => x.DateOfBirth).NotNull().NotEmpty();
        }
    }
}