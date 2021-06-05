using System;
using MediatR;

namespace Patterns.Mediator.ConsoleApp.Messages
{
    public class CustomerUpdatedEvent : INotification
    {
        public DateTime UpdatedDateTime { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}