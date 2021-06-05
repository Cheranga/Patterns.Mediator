using System;
using System.Reflection.Metadata;
using MediatR;
using Patterns.Mediator.ConsoleApp.Core;

namespace Patterns.Mediator.ConsoleApp.DTO
{
    public class GetCustomerByEmailRequest : IRequest<Result<GetCustomerResponse>>
    {
        public string Email { get; set; }
    }
}