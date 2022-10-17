using Mc2.CrudTest.Presentation.Domain.AggregatesModel.CustomerAggregate;
using System;
using Xunit;

namespace Mc2.CrudTest.AcceptanceTests
{
    public class CustomerAggregateTest
    {
        [Fact]
        public void Create_customer_success()
        {
            //Arrange    
            var firstname = "fakeFirstName";
            var lastname = "fakeLastname";
            var dateOfBirth = DateTime.UtcNow;
            var phoneNumber = "+44 117 496 0123";
            var email = "test@test.com";
            var bankAccountNumber = "0000-0000-0000-0000";

            //Act 
            var fakeCustomer = new Customer(firstname, lastname, dateOfBirth, phoneNumber, email, bankAccountNumber);

            //Assert
            Assert.NotNull(fakeCustomer);
        }

        [Fact]
        public void Ivalid_phonenumber_fail()
        {
            //Arrange    
            var firstname = "fakeFirstName";
            var lastname = "fakeLastname";
            var dateOfBirth = DateTime.UtcNow;
            var phoneNumber = "117 496 0123";
            var email = "test@test.com";
            var bankAccountNumber = "0000-0000-0000-0000";            

            //Act - Assert
            Assert.Throws<ArgumentException>(() => new Customer(firstname, lastname, dateOfBirth, phoneNumber, email, bankAccountNumber));
        }

        [Fact]
        public void Ivalid_email_fail()
        {
            //Arrange    
            var firstname = "fakeFirstName";
            var lastname = "fakeLastname";
            var dateOfBirth = DateTime.UtcNow;
            var phoneNumber = "+44 117 496 0123";
            var email = "test@";
            var bankAccountNumber = "0000-0000-0000-0000";

            //Act - Assert
            Assert.Throws<ArgumentException>(() => new Customer(firstname, lastname, dateOfBirth, phoneNumber, email, bankAccountNumber));
        }

        [Fact]
        public void Ivalid_bankAccount_fail()
        {
            //Arrange    
            var firstname = "fakeFirstName";
            var lastname = "fakeLastname";
            var dateOfBirth = DateTime.UtcNow;
            var phoneNumber = "+44 117 496 0123";
            var email = "test@test.com";
            var bankAccountNumber = "00000000";

            //Act - Assert
            Assert.Throws<ArgumentException>(() => new Customer(firstname, lastname, dateOfBirth, phoneNumber, email, bankAccountNumber));
        }

        [Fact]
        public void Ivalid_firstname_fail()
        {
            //Arrange    
            var firstname = "";
            var lastname = "fakeLastname";
            var dateOfBirth = DateTime.UtcNow;
            var phoneNumber = "+44 117 496 0123";
            var email = "test@test.com";
            var bankAccountNumber = "0000-0000-0000-0000";

            //Act - Assert
            Assert.Throws<ArgumentNullException>(() => new Customer(firstname, lastname, dateOfBirth, phoneNumber, email, bankAccountNumber));
        }
        // Please create more tests based on project requirements as per in readme.md
    }
}
