using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Patterns.Mediator.ConsoleApp.Core;
using Patterns.Mediator.ConsoleApp.DTO;
using Patterns.Mediator.ConsoleApp.Messages;
using Patterns.Mediator.ConsoleApp.Publisher;

namespace Patterns.Mediator.ConsoleApp.Services
{
    public class MediatorCustomerService : ICustomerService
    {
        private readonly IMediator _mediator;
        private readonly IAsyncPublisher _asyncPublisher;
        private readonly ILogger<MediatorCustomerService> _logger;

        public MediatorCustomerService(IMediator mediator, IAsyncPublisher asyncPublisher, ILogger<MediatorCustomerService> logger)
        {
            _mediator = mediator;
            _asyncPublisher = asyncPublisher;
            _logger = logger;
        }

        public async Task<Result<GetCustomerResponse>> SearchAsync(GetCustomerByEmailRequest request)
        {
            var operation = await _mediator.Send(request);
            return operation;
        }

        public async Task<Result<GetCustomerResponse>> SearchAsync(GetCustomerByIdRequest request)
        {
            var operation = await _mediator.Send(request);
            return operation;
        }

        public Task CreateCustomerAsync(CreateCustomerRequest request)
        {
            return _mediator.Send(request);
        }

        public async Task<Result<GetCustomerResponse>> UpdateCustomerAsync(UpdateCustomerRequest request)
        {
            var getCustomerRequest = new GetCustomerByIdRequest
            {
                Id = request.Id
            };

            var getCustomerOperation = await SearchAsync(getCustomerRequest);
            if (!getCustomerOperation.Status)
            {
                return Result<GetCustomerResponse>.Failure(getCustomerOperation.ErrorCode, getCustomerOperation.ValidationResult);
            }

            var customer = getCustomerOperation.Data;
            if (customer == null)
            {
                return Result<GetCustomerResponse>.Failure("CUSTOMER_NOT_FOUND", "customer is not found");
            }

            var updateCustomerRequest = new UpdateCustomerRequest
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth
            };

            var updateCustomerOperation = await _mediator.Send(updateCustomerRequest);
            if (!updateCustomerOperation.Status || updateCustomerOperation.Data == null)
            {
                return updateCustomerOperation;
            }

            var updatedCustomer = updateCustomerOperation.Data;

            var updatedCustomerEvent = new CustomerUpdatedEvent
            {
                Id = request.Id,
                UpdatedDateTime = DateTime.UtcNow,
                FirstName = updatedCustomer.Data.FirstName,
                LastName = updatedCustomer.Data.LastName,
                DateOfBirth = updatedCustomer.Data.DateOfBirth,
            };

            try
            {
                //await _mediator.Publish(updatedCustomerEvent);
                await _asyncPublisher.Publish(updatedCustomerEvent, PublishStrategy.ParallelWhenAll, CancellationToken.None);
            }
            catch (NotificationHandlerException exception)
            {
                _logger.LogError(exception, "Error occured when handling the customer updated event.");
                return Result<GetCustomerResponse>.Failure(exception.ErrorCode, "Error occurred when handling events");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error occured when handling the customer updated event.");
                return Result<GetCustomerResponse>.Failure("SERVER_ERROR", "Internal server error");
            }

            return updateCustomerOperation;
        }
    }
}