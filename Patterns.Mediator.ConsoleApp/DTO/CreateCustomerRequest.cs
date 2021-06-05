using MediatR;

namespace Patterns.Mediator.ConsoleApp.DTO
{
    public class CreateCustomerRequest : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
    }
}