using Mc2.CrudTest.Presentation.Application.Commands;
using Mc2.CrudTest.Presentation.Domain.AggregatesModel.CustomerAggregate;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Mc2.CrudTest.FunctionalTests
{
    public class CustomerScenarios : CustomerScenarioBase
    {
        private readonly string _baseUrl = "api/v1/Customers";

        #region Create
        [Fact]
        public async Task Create_new_customer_and_response_ok_status_code()
        {
            using var server = CreateServer();

            var content = new StringContent(BuildCustomer(), UTF8Encoding.UTF8, "application/json");

            var response = await server.CreateClient().PostAsync(_baseUrl, content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Create_new_customer_and_response_internalserver_status_code()
        {
            using var server = CreateServer();

            var content = new StringContent(BuildBadCustomer(), UTF8Encoding.UTF8, "application/json");

            var response = await server.CreateClient().PostAsync(_baseUrl, content);

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        #endregion

        #region Delete
        [Fact]
        public async Task Delete_customer_and_response_ok_status_code()
        {
            using var server = CreateServer();

            var content = new StringContent(BuildCustomer(), UTF8Encoding.UTF8, "application/json");

            var response = await server.CreateClient().PostAsync(_baseUrl, content);

            var deleteResponse = await server.CreateClient().DeleteAsync($"{_baseUrl}/1");

            deleteResponse.EnsureSuccessStatusCode();
        }



        [Fact]
        public async Task Delete_customer_and_response_notfound_status_code()
        {
            using var server = CreateServer();

            var response = await server.CreateClient().DeleteAsync($"{_baseUrl}/1");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        #endregion

        #region Get
        [Fact]
        public async Task Get_customer_and_response_notfound_status_code()
        {
            using var server = CreateServer();

            var response = await server.CreateClient().GetAsync($"{_baseUrl}/1");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Get_customers_and_response_ok_status_code()
        {
            using var server = CreateServer();

            var response = await server.CreateClient().GetAsync(_baseUrl);

            response.EnsureSuccessStatusCode();
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_customers_and_response_ok_status_code()
        {
            using var server = CreateServer();

            var content = new StringContent(BuildCustomer(), UTF8Encoding.UTF8, "application/json");

            var response = await server.CreateClient().PostAsync(_baseUrl, content);

            var secondContent = new StringContent(BuildSecondCustomer(), UTF8Encoding.UTF8, "application/json");

            var updateResponse = await server.CreateClient().PutAsync($"{_baseUrl}/1", secondContent);

            updateResponse.EnsureSuccessStatusCode();

        }

        [Fact]
        public async Task Update_customers_and_response_notfound_status_code()
        {
            using var server = CreateServer();

            var secondContent = new StringContent(BuildSecondCustomer(), UTF8Encoding.UTF8, "application/json");

            var updateResponse = await server.CreateClient().PutAsync($"{_baseUrl}/1", secondContent);

            Assert.Equal(HttpStatusCode.NotFound, updateResponse.StatusCode);
        }
        #endregion



        string BuildCustomer()
        {
            //Arrange    
            var firstname = "fakeFirstName";
            var lastname = "fakeLastname";
            var dateOfBirth = DateTime.UtcNow;
            var phoneNumber = "+441174960123";
            var email = "test@test.com";
            var bankAccountNumber = "0000-0000-0000-0000";

            //Act 
            var customer = new CreateCustomerCommand
            {
                Firstname = firstname,
                Lastname = lastname,
                DateOfBirth = dateOfBirth,
                PhoneNumber = phoneNumber,
                Email = email,
                BankAccountNumber = bankAccountNumber
            };

            return JsonSerializer.Serialize(customer);
        }
        string BuildSecondCustomer()
        {
            //Arrange    
            var firstname = "fakeFirstName2";
            var lastname = "fakeLastname2";
            var dateOfBirth = DateTime.UtcNow;
            var phoneNumber = "+441174960124";
            var email = "test2@test.com";
            var bankAccountNumber = "0000-0000-0000-0000";

            //Act 
            var customer = new UpdateCustomerCommand
            {
                Id = 1,
                Firstname = firstname,
                Lastname = lastname,
                DateOfBirth = dateOfBirth,
                PhoneNumber = phoneNumber,
                Email = email,
                BankAccountNumber = bankAccountNumber
            };

            return JsonSerializer.Serialize(customer);
        }
        string BuildBadCustomer()
        {
            //Arrange    
            var firstname = "fakeFirstName";
            var lastname = "fakeLastname";
            var dateOfBirth = DateTime.UtcNow;
            var phoneNumber = "+441174960123";
            var email = "test.com";
            var bankAccountNumber = "0000-0000-0000-0000";

            //Act 
            var customer = new CreateCustomerCommand
            {
                Firstname = firstname,
                Lastname = lastname,
                DateOfBirth = dateOfBirth,
                PhoneNumber = phoneNumber,
                Email = email,
                BankAccountNumber = bankAccountNumber
            };

            return JsonSerializer.Serialize(customer);
        }
    }
}
