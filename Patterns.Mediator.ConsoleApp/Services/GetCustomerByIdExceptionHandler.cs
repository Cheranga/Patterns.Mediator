using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Patterns.Mediator.ConsoleApp.Core;
using Patterns.Mediator.ConsoleApp.DTO;

namespace Patterns.Mediator.ConsoleApp.Services
{
    public class GetCustomerByIdExceptionHandler : AsyncRequestExceptionHandler<GetCustomerByIdRequest, Result<GetCustomerResponse>>
    {
        protected override Task Handle(GetCustomerByIdRequest request, Exception exception, RequestExceptionHandlerState<Result<GetCustomerResponse>> state, CancellationToken cancellationToken)
        {
            state.SetHandled(Result<GetCustomerResponse>.Failure("GET_CUSTOMER_SEARCH_BY_ID", "error occurred when searching for customer by id"));

            return Task.CompletedTask;
        }
    }
}