using System.Threading;
using System.Transactions;
using FluentValidation;
using FluentValidation.Results;

namespace Patterns.Mediator.ConsoleApp.Validators
{
    public abstract class ModelValidatorBase<TModel> : AbstractValidator<TModel> where TModel:class
    {
        protected override bool PreValidate(ValidationContext<TModel> context, ValidationResult result)
        {
            var instance = context.InstanceToValidate;
            if (instance != null)
            {
                return true;
            }

            result.Errors.Add(new ValidationFailure("Instance", "Instance cannot be null"));
            return false;

        }
    }
}