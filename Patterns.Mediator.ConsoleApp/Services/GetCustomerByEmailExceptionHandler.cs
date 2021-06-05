using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Patterns.Mediator.ConsoleApp.Core;
using Patterns.Mediator.ConsoleApp.DTO;
using Patterns.Mediator.ConsoleApp.Messages;

namespace Patterns.Mediator.ConsoleApp.Services
{
    public class GetCustomerByEmailExceptionHandler : AsyncRequestExceptionHandler<GetCustomerByEmailRequest, Result<GetCustomerResponse>>
    {
        private readonly ILogger<GetCustomerByEmailExceptionHandler> _logger;

        public GetCustomerByEmailExceptionHandler(ILogger<GetCustomerByEmailExceptionHandler> logger)
        {
            _logger = logger;
        }

        protected override Task Handle(GetCustomerByEmailRequest request, Exception exception, RequestExceptionHandlerState<Result<GetCustomerResponse>> state, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "exception occurred when searching for customer by email");

            if (exception is CustomerSearchException customerSearchException)
            {
                state.SetHandled(Result<GetCustomerResponse>.Failure(customerSearchException.ErrorCode, customerSearchException.Message));
            }

            return Task.CompletedTask;
        }
    }
}