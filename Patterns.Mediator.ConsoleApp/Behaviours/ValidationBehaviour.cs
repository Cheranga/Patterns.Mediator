using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Patterns.Mediator.ConsoleApp.Core;

namespace Patterns.Mediator.ConsoleApp.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>>
    {
        private readonly IValidator<TRequest> _validator;

        public ValidationBehaviour(IValidator<TRequest> validator = null)
        {
            _validator = validator;
        }

        public async Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Result<TResponse>> next)
        {
            if (_validator == null)
            {
                return await next();
            }

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid)
            {
                return await next();
            }

            return Result<TResponse>.Failure("INVALID_REQUEST", validationResult);
        }
    }
}