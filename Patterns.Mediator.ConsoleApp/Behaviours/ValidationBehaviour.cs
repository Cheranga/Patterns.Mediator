using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Patterns.Mediator.ConsoleApp.Core;

namespace Patterns.Mediator.ConsoleApp.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>>
    {
        private readonly ILogger<ValidationBehaviour<TRequest, TResponse>> _logger;
        private readonly IValidator<TRequest> _validator;

        public ValidationBehaviour(ILogger<ValidationBehaviour<TRequest, TResponse>> logger, IValidator<TRequest> validator = null)
        {
            _logger = logger;
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

            _logger.LogError("Validation error when handling request {Request}", typeof(TRequest).Name);
            return Result<TResponse>.Failure("INVALID_REQUEST", validationResult);
        }
    }
}