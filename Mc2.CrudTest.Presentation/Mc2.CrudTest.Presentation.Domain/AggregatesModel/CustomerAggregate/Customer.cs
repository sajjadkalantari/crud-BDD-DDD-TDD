using Mc2.CrudTest.Presentation.Domain.Infrustructure;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Mc2.CrudTest.Presentation.Domain.AggregatesModel.CustomerAggregate
{

    public class Customer : Entity, IAggregateRoot
    {

        private Customer() { }

        public Customer(string firstname, string lastname, DateTime dateOfBirth, string phoneNumber, string email, string bankAccountNumber)
        {
            Firstname = !string.IsNullOrEmpty(firstname) ? firstname.ToLower() : throw new ArgumentNullException(nameof(firstname));
            Lastname = !string.IsNullOrEmpty(lastname) ? lastname.ToLower() : throw new ArgumentNullException(nameof(lastname));
            DateOfBirth = dateOfBirth;
            PhoneNumber = new PhoneNumberValueObject(phoneNumber);
            Email = new EmailValueObject(email);
            BankAccountNumber = new BankAccountNumberValueObject(bankAccountNumber);
        }
        public Customer(int id, string firstname, string lastname, DateTime dateOfBirth, string phoneNumber, string email, string bankAccountNumber) : this(firstname, lastname, dateOfBirth, phoneNumber, email, bankAccountNumber)
        {
            Id = id;
        }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public PhoneNumberValueObject PhoneNumber { get; set; }
        public EmailValueObject Email { get; set; }
        public BankAccountNumberValueObject BankAccountNumber { get; set; }

    }
}
