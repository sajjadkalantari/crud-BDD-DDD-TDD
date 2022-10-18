
using Mc2.CrudTest.Presentation.Application.Commands;
using Mc2.CrudTest.Presentation.Application.Dtos;
using Mc2.CrudTest.Presentation.Application.Queries;
using Mc2.CrudTest.Presentation.Server.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Mc2.CrudTest.AcceptanceTests.Application
{
    public class CustomersWebApiTest
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<ICustomerQueries> _customerQueriesMock;

        public CustomersWebApiTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _customerQueriesMock = new Mock<ICustomerQueries>();
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
            var customerController = new CustomersController(_mediatorMock.Object, _customerQueriesMock.Object);
            var actionResult = (await customerController.CreateCustomerAsync(customerCommand));

            //Assert            
            Assert.Equal(actionResult.Value, customerDto);

        }

        [Fact]
        public async Task Delete_customer_success()
        {
            //Arrange
            var customerId = 1;
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteCustomerCommand>(), default(CancellationToken)))
                .Returns(Task.FromResult(customerId));

            //Act
            var customerController = new CustomersController(_mediatorMock.Object, _customerQueriesMock.Object);
            var actionResult = (await customerController.DeleteCustomerAsync(customerId));

            //Assert            
            Assert.Equal(actionResult.Value, customerId);

        }

        [Fact]
        public async Task Get_customers_success()
        {
            //Arrange
            var customers = new List<CustomerDTO> {
                new CustomerDTO
            {
                Firstname = "fakeFirstName",
                Lastname = "fakeLastname",
                DateOfBirth = DateTime.UtcNow,
                PhoneNumber = "+44 117 496 0123",
                Email = "test@test.com",
                BankAccountNumber = "0000-0000-0000-0000",
            },
                new CustomerDTO
                {
                    Firstname = "fakeFirstName2",
                    Lastname = "fakeLastname2",
                    DateOfBirth = DateTime.UtcNow,
                    PhoneNumber = "+44 117 496 0122",
                    Email = "test2@test.com",
                    BankAccountNumber = "0000-0000-0000-0000",
                },
             };
            _customerQueriesMock.Setup(x => x.GetCustomersAsync())
                .Returns(Task.FromResult(customers));

            //Act
            var customerController = new CustomersController(_mediatorMock.Object, _customerQueriesMock.Object);
            var actionResult = await customerController.GetAllCustomersAsync();

            //Assert            
            Assert.Equal(((OkObjectResult)actionResult.Result).Value, customers);

        }
    }
}
