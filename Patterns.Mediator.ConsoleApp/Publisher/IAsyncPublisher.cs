using System.Threading;
using System.Threading.Tasks;

namespace Patterns.Mediator.ConsoleApp.Publisher
{
    public interface IAsyncPublisher
    {
        Task Publish<TNotification>(TNotification notification, PublishStrategy strategy, CancellationToken cancellationToken);
    }
}