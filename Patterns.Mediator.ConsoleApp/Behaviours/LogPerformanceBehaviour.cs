using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Patterns.Mediator.ConsoleApp.Behaviours
{
    public class LogPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest:IRequest<TResponse> where TResponse:class
    {
        private readonly ILogger<LogPerformanceBehaviour<TRequest, TResponse>> _logger;

        public LogPerformanceBehaviour(ILogger<LogPerformanceBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            _logger.LogInformation("{StartingTime}::{RequestType} handling started", DateTime.UtcNow.ToString("HH:mm:ss"), typeof(TRequest).Name);

            var response = await next();

            stopWatch.Stop();

            _logger.LogInformation("{FinishingTime}::{RequestType} handling finished. Time taken {TimeTaken}ms", DateTime.UtcNow.ToString("HH:mm:ss"), typeof(TRequest).Name, stopWatch.ElapsedMilliseconds);

            return response;
        }
    }
}
