using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Patterns.Mediator.ConsoleApp.Core;
using Patterns.Mediator.ConsoleApp.DTO;

namespace Patterns.Mediator.ConsoleApp.Services
{
    public class GetCustomerByEmailRequestHandler : IRequestHandler<GetCustomerByEmailRequest, Result<GetCustomerResponse>>
    {
        private readonly IValidator<GetCustomerByEmailRequest> _validator;

        public GetCustomerByEmailRequestHandler(IValidator<GetCustomerByEmailRequest> validator)
        {
            _validator = validator;
        }


        public async Task<Result<GetCustomerResponse>> Handle(GetCustomerByEmailRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result<GetCustomerResponse>.Failure("INVALID_REQUEST", validationResult);
            }

            return Result<GetCustomerResponse>.Success(new GetCustomerResponse
            {
                Data = new CustomerDto
                {
                    FirstName = "Cheranga",
                    LastName = "Hatangala",
                    DateOfBirth = new DateTime(1982, 11, 1)
                }
            });
        }
    }
}