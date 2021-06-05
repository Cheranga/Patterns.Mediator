using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Patterns.Mediator.ConsoleApp.Messages
{
    public class BirthDayGreeterHandler : INotificationHandler<CustomerUpdatedEvent>
    {
        private readonly ILogger<BirthDayGreeterHandler> _logger;

        public BirthDayGreeterHandler(ILogger<BirthDayGreeterHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(CustomerUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started birthday service.");
            var dateTime = notification.DateOfBirth;
            // TODO: Update the system to send birthday greetings.

            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);

            //throw new Exception("Birthday service error!");
            throw new NotificationHandlerException(notification, "BIRTHDAY_SERVICE_ERROR", "error occurred when updating the birthday service");

            _logger.LogInformation("Finished birthday service.");
        }
    }
}