using Mc2.CrudTest.IntegrationTests.Api;
using Mc2.CrudTest.Presentation.Application.Commands;
using Mc2.CrudTest.Presentation.Application.Dtos;
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

            _scenarioContext["customer"] = await _customerApi.CreateCustomerAsync(customer);
        }

        [Then(@"user can lookup all customers and filter by email of ""([^""]*)"" and get ""([^""]*)"" records")]
        public async Task ThenUserCanLookupAllCustomersAndFilterByEmailOfAndGetRecords(string p0, string p1)
        {
            var customers = await _customerApi.GetCustomersAsync();
            var customerCount = customers.Where(x => x.Email == p0).Count();
            Assert.Equal(p1, customerCount.ToString());
        }

        [When(@"user edit customer with new email of ""([^""]*)""")]
        public void WhenUserEditCustomerWithNewEmailOf(string p0)
        {
            var custopmer = (CustomerDTO)_scenarioContext["customer"];

            throw new PendingStepException();
        }


        [When(@"user delete customer by Email of ""([^""]*)""")]
        public void WhenUserDeleteCustomerByEmailOf(string p0)
        {
            throw new PendingStepException();
        }
    }
}
