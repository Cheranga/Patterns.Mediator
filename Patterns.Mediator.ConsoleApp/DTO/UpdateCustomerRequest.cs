using MediatR;
using Patterns.Mediator.ConsoleApp.Core;

namespace Patterns.Mediator.ConsoleApp.DTO
{
    public class UpdateCustomerRequest : IRequest<Result<GetCustomerResponse>>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
    }
}