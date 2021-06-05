using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Patterns.Mediator.ConsoleApp.Core;
using Patterns.Mediator.ConsoleApp.DTO;

namespace Patterns.Mediator.ConsoleApp.Services
{
    public class UpdateCustomerRequestHandler : IRequestHandler<UpdateCustomerRequest, Result<GetCustomerResponse>>
    {
        private readonly IValidator<UpdateCustomerRequest> _validator;

        public UpdateCustomerRequestHandler(IValidator<UpdateCustomerRequest> validator)
        {
            _validator = validator;
        }

        public async Task<Result<GetCustomerResponse>> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result<GetCustomerResponse>.Failure("INVALID_COMMAND", validationResult);
            }

            // TODO: Update the customer
            return Result<GetCustomerResponse>.Success(new GetCustomerResponse
            {
                Data = new CustomerDto
                {
                    FirstName = "Cheranga updated",
                    LastName = "Hatangala updated",
                    DateOfBirth = new DateTime(1982, 11, 1)
                }
            });
        }
    }
}