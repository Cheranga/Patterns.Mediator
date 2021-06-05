using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Patterns.Mediator.ConsoleApp.Messages
{
    public class LoyaltyPointsHandler : INotificationHandler<CustomerUpdatedEvent>
    {
        private readonly ILogger<LoyaltyPointsHandler> _logger;

        public LoyaltyPointsHandler(ILogger<LoyaltyPointsHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(CustomerUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started loyalty service.");
            // TODO: Simulate the system updates in the third party system.

            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);

            _logger.LogInformation("Finished loyalty service.");
        }
    }
}