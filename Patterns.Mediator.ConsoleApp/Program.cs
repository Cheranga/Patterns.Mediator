using System;
using System.Diagnostics;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Patterns.Mediator.ConsoleApp.Behaviours;
using Patterns.Mediator.ConsoleApp.Core;
using Patterns.Mediator.ConsoleApp.DTO;
using Patterns.Mediator.ConsoleApp.Publisher;
using Patterns.Mediator.ConsoleApp.Services;
using Patterns.Mediator.ConsoleApp.Validators;

namespace Patterns.Mediator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {  
            var provider = GetServiceProvider();
            var customerService = provider.GetRequiredService<ICustomerService>();

            var getCustomerByEmailOperation = customerService.SearchAsync(new GetCustomerByEmailRequest
            {
                Email = ""
            }).GetAwaiter().GetResult();

            var getCustomerByIdOperation = customerService.SearchAsync(new GetCustomerByIdRequest
            {
                Id = ""//Guid.NewGuid().ToString("N")
            }).GetAwaiter().GetResult();

            customerService.CreateCustomerAsync(new CreateCustomerRequest
            {
                FirstName = "Cheranga",
                LastName = "Hatangala",
                DateOfBirth = "1982/11/01"
            }).GetAwaiter().GetResult();

            var updateCustomerOperation = customerService.UpdateCustomerAsync(new UpdateCustomerRequest
            {
                Id = Guid.NewGuid().ToString("N"),
                FirstName = "Cheranga",
                LastName = "Hatangala",
                DateOfBirth = "1982/11/01"
            }).GetAwaiter().GetResult();

            Console.ReadLine();

        }

        static async Task<Result<GetCustomerResponse>> GetCustomerResponse(GetCustomerByEmailRequest request)
        {
            var serviceProvider = GetServiceProvider();
            var customerService = serviceProvider.GetRequiredService<ICustomerSearchService>();

            var operation = await customerService.SearchAsync(request);
            return operation;
        }

        private static IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();

            RegisterDependencies(services);

            var serviceProvider = services.BuildServiceProvider();
            
            return serviceProvider;
        }

        private static void RegisterDependencies(ServiceCollection services)
        {
            var assemblies = new[]
            {
                typeof(ModelValidatorBase<>).Assembly
            };

            services.AddValidatorsFromAssemblies(assemblies);
            services.AddMediatR(assemblies);
            
            services.AddTransient<IPipelineBehavior<GetCustomerByEmailRequest, Result<GetCustomerResponse>>, ValidationBehaviour<GetCustomerByEmailRequest, GetCustomerResponse>>();
            services.AddTransient<IPipelineBehavior<GetCustomerByIdRequest, Result<GetCustomerResponse>>, ValidationBehaviour<GetCustomerByIdRequest, GetCustomerResponse>>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LogPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionProcessorBehavior<,>));

            services.AddLogging(builder => builder.AddConsole());

            services.AddSingleton<ICustomerService, MediatorCustomerService>();
            services.AddSingleton<IAsyncPublisher, AsyncPublisher>();

            services.AddHttpClient<IExternalCustomerSearchService, ExternalCustomerSearchService>();
        }
    }
}