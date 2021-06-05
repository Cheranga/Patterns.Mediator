using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Patterns.Mediator.ConsoleApp.Core;
using Patterns.Mediator.ConsoleApp.DTO;
using Patterns.Mediator.ConsoleApp.Messages;

namespace Patterns.Mediator.ConsoleApp.Services
{
    public class GetCustomerByEmailExceptionHandler : AsyncRequestExceptionHandler<GetCustomerByEmailRequest, Result<GetCustomerResponse>>
    {
        protected override Task Handle(GetCustomerByEmailRequest request, Exception exception, RequestExceptionHandlerState<Result<GetCustomerResponse>> state, CancellationToken cancellationToken)
        {
            if (exception is CustomerSearchException customerSearchException)
            {
                state.SetHandled(Result<GetCustomerResponse>.Failure(customerSearchException.ErrorCode, customerSearchException.Message));
            }

            return Task.CompletedTask;
        }
    }
}