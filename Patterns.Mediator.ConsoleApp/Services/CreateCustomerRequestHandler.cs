using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Patterns.Mediator.ConsoleApp.DTO;

namespace Patterns.Mediator.ConsoleApp.Services
{
    public class CreateCustomerRequestHandler : AsyncRequestHandler<CreateCustomerRequest>
    {
        private readonly ILogger<CreateCustomerRequestHandler> _logger;
        private readonly IValidator<CreateCustomerRequest> _validator;

        public CreateCustomerRequestHandler(IValidator<CreateCustomerRequest> validator, ILogger<CreateCustomerRequestHandler> logger)
        {
            _validator = validator;
            _logger = logger;
        }

        protected override async Task Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                _logger.LogError("INVALID_COMMAND {Command}", nameof(CreateCustomerRequest));
            }

            // TODO: Create the customer
        }
    }
}