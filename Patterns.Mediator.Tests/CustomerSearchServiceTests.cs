using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Patterns.Mediator.ConsoleApp;
using TestStack.BDDfy;
using Xunit;

namespace Patterns.Mediator.Tests
{
    public class CustomerSearchServiceTests
    {
        private readonly CustomerSearchService _customerSearchService;
        private Mock<IValidator<GetCustomerByEmailRequest>> _validator;
        private readonly Fixture _fixture;
        private GetCustomerByEmailRequest _request;
        private Result<GetCustomerResponse> _operation;

        public CustomerSearchServiceTests()
        {
            _fixture = new Fixture();
            _validator = new Mock<IValidator<GetCustomerByEmailRequest>>();

            _customerSearchService = new CustomerSearchService(_validator.Object);
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public Task GetCustomerByIdRequestIsInvalid(string email)
        {
            this.Given(x => GivenInvalidGetCustomerByIdRequest(email))
                .When(x => WhenSearchIsPerformed())
                .Then(x => ThenMustReturnFailure())
                .BDDfy();

            return Task.CompletedTask;
        }

        private void ThenMustReturnFailure()
        {
            _operation.Status.Should().BeFalse();
            _operation.ErrorCode.Should().Be("INVALID_REQUEST");
        }

        private async Task WhenSearchIsPerformed()
        {
            _operation = await _customerSearchService.SearchAsync(_request);
        }

        private void GivenInvalidGetCustomerByIdRequest(string email)
        {
            _request = _fixture.Create<GetCustomerByEmailRequest>();
            _request.Email = email;

            _validator.Setup(x => x.ValidateAsync(_request, It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult(new[] {new ValidationFailure("invalidrequest", "invalidrequest")}));
        }
    }
}