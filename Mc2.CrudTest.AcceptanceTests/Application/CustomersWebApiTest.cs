
using Mc2.CrudTest.Presentation.Application.Commands;
using Mc2.CrudTest.Presentation.Application.Dtos;
using Mc2.CrudTest.Presentation.Server.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Mc2.CrudTest.AcceptanceTests.Application
{
    public class CustomersWebApiTest
    {
        private Mock<IMediator> _mediatorMock;

        public CustomersWebApiTest()
        {
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task Create_customer_success()
        {
            //Arrange
            var customerCommand = new CreateCustomerCommand
            {
                Firstname = "fakeFirstName",
                Lastname = "fakeLastname",
                DateOfBirth = DateTime.UtcNow,
                PhoneNumber = "+44 117 496 0123",
                Email = "test@test.com",
                BankAccountNumber = "0000-0000-0000-0000",
            };

            var customerDto = CustomerDTO.FromCustomer(CreateCustomerCommand.ToCustomer(customerCommand));

            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateCustomerCommand>(), default(CancellationToken)))
                .Returns(Task.FromResult(customerDto));

            //Act
            var customerController = new CustomersController(_mediatorMock.Object);
            var actionResult = (await customerController.CreateCustomerAsync(customerCommand));

            //Assert            
            Assert.Equal(actionResult.Value, customerDto);

        }
    }
}
