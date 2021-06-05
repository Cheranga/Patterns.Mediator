using System.Threading.Tasks;
using MediatR;
using Patterns.Mediator.ConsoleApp.Core;
using Patterns.Mediator.ConsoleApp.DTO;

namespace Patterns.Mediator.ConsoleApp.Services
{
    public class MediatorCustomerService : ICustomerService
    {
        private readonly IMediator _mediator;

        public MediatorCustomerService(IMediator mediator)
        {
            _mediator = mediator;
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

            return updateCustomerOperation;
        }
    }
}