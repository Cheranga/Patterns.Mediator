using System;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Patterns.Mediator.ConsoleApp.Validators;

namespace Patterns.Mediator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var getCustomerByEmailRequest = new GetCustomerByEmailRequest
            {
                Email = "cheranga@gmail.com"
            };

            var response = GetCustomerResponse(getCustomerByEmailRequest).GetAwaiter().GetResult();
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
        }
    }
}