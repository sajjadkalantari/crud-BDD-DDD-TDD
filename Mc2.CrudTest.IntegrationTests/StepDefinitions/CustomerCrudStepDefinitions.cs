using Mc2.CrudTest.IntegrationTests.Api;
using Mc2.CrudTest.Presentation.Application.Commands;
using Mc2.CrudTest.Presentation.Application.Dtos;
using Mc2.CrudTest.Shared.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace Mc2.CrudTest.IntegrationTests.StepDefinitions
{
    [Binding]
    public class CustomerCrudStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly CustomerApi _customerApi;

        public CustomerCrudStepDefinitions(ScenarioContext scenarioContext, CustomerApi customerApi)
        {
            _scenarioContext = scenarioContext;
            _customerApi = customerApi;
        }
        [Given(@"system erro codes are following")]
        public void GivenSystemErroCodesAreFollowing(Table table)
        {
            _scenarioContext["ErrorCodes"] = table;
        }

        [When(@"user creates a customer with following data")]
        public async Task WhenUserCreatesACustomerWithFollowingData(Table table)
        {
            var customer = new CreateCustomerCommand
            {
                Firstname = table.Rows[0]["Firstname"],
                Lastname = table.Rows[0]["Lastname"],
                Email = table.Rows[0]["Email"],
                PhoneNumber = table.Rows[0]["Phonenumber"],
                DateOfBirth = DateTime.Parse(table.Rows[0]["DateOfBirth"]),
                BankAccountNumber = table.Rows[0]["BankAccountNumber"]
            };
            try
            {
                _scenarioContext["customer"] = await _customerApi.CreateCustomerAsync(customer);
            }
            catch (CustomSystemException e)
            {
                _scenarioContext["customer-error"] = e;
            }
        }

        [Then(@"user can lookup all customers and filter by email of ""([^""]*)"" and get ""([^""]*)"" records")]
        public async Task ThenUserCanLookupAllCustomersAndFilterByEmailOfAndGetRecords(string p0, string p1)
        {
            var customers = await _customerApi.GetCustomersAsync();
            var customerCount = customers.Where(x => x.Email == p0).Count();
            Assert.Equal(p1, customerCount.ToString());
        }

        [When(@"user edit customer with new email of ""([^""]*)""")]
        public async Task WhenUserEditCustomerWithNewEmailOf(string p0)
        {
            var customer = (CustomerDTO)_scenarioContext["customer"];

            var updateCommand = new UpdateCustomerCommand
            {
                Id = customer.Id,
                BankAccountNumber = customer.BankAccountNumber,
                Email = p0,
                Firstname = customer.Firstname,
                Lastname = customer.Lastname,
                DateOfBirth = customer.DateOfBirth,
                PhoneNumber = customer.PhoneNumber
            };

            _scenarioContext["customer"] = await _customerApi.UpdateCustomerAsync(updateCommand);
        }


        [When(@"user delete customer by Email of ""([^""]*)""")]
        public async Task WhenUserDeleteCustomerByEmailOf(string p0)
        {
            var customers = await _customerApi.GetCustomersAsync();
            var customer = customers.Where(x => x.Email == p0).First();
            await _customerApi.DeleteCustomersAsync(customer.Id);
        }

        [Then(@"user should get and error with code ""([^""]*)"" and proper error message")]
        public async Task ThenUserShouldGetAndErrorWithCodeAndMessage(string p0)
        {
            var customerCreationError = (CustomSystemException)_scenarioContext["customer-error"];
            var errorMessage = ((Table)_scenarioContext["ErrorCodes"]).Rows.First(m => m["Code"] == p0)["Description"];

            Assert.Equal(p0, customerCreationError.Code.ToString());
            Assert.Equal(errorMessage, customerCreationError.Message);
        }

    }
}
