using FluentValidation;
using Patterns.Mediator.ConsoleApp.DTO;

namespace Patterns.Mediator.ConsoleApp.Validators
{
    public class UpdateCustomerRequestValidator : ModelValidatorBase<UpdateCustomerRequest>
    {
        public UpdateCustomerRequestValidator()
        {
            RuleFor(x=>x.Id).NotNull().NotEmpty();
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.LastName).NotNull().NotEmpty();
            RuleFor(x => x.DateOfBirth).NotNull().NotEmpty();
        }
    }
}