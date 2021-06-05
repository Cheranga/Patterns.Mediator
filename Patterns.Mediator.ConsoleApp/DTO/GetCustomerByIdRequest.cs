using System.Reflection.Metadata;
using MediatR;
using Patterns.Mediator.ConsoleApp.Core;

namespace Patterns.Mediator.ConsoleApp.DTO
{
    public class GetCustomerByIdRequest : IRequest<Result<GetCustomerResponse>>
    {
        public string Id { get; set; }
    }
}