using FluentValidation;
using Patterns.Mediator.ConsoleApp.DTO;

namespace Patterns.Mediator.ConsoleApp.Validators
{
    public class GetCustomerByIdRequestValidator : ModelValidatorBase<GetCustomerByIdRequest>
    {
        public GetCustomerByIdRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}