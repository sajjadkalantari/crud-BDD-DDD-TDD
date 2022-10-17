using Mc2.CrudTest.Presentation.Domain.Infrustructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Domain.AggregatesModel.CustomerAggregate
{
    public class Customer: Entity, IAggregateRoot
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }

        public Customer(string firstname, string lastname, DateTime dateOfBirth, string phoneNumber, string email, string bankAccountNumber)
        {
            Firstname = !string.IsNullOrEmpty(firstname) ? firstname : throw new ArgumentNullException(nameof(firstname));
            Lastname = !string.IsNullOrEmpty(lastname) ? lastname : throw new ArgumentNullException(nameof(lastname));
            DateOfBirth = dateOfBirth;
            PhoneNumber = ValidatePhonenumber(phoneNumber) ? phoneNumber : throw new ArgumentException(nameof(phoneNumber));
            Email = ValidateEmail(email) ? email : throw new ArgumentException(nameof(email));
            BankAccountNumber = ValidateBankAccount(bankAccountNumber) ? bankAccountNumber : throw new ArgumentException(nameof(bankAccountNumber));
        }

        private bool ValidatePhonenumber(string phonenumber)
        {
            try
            {
                if (string.IsNullOrEmpty(phonenumber)) return false;
                var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
                var phonenumberInstance = phoneNumberUtil.Parse(phonenumber, null);
                return phoneNumberUtil.IsValidNumber(phonenumberInstance);
            }
            catch (Exception)
            {
                return false;
            }
        
        }

        private bool ValidateEmail(string email)
        {
            try
            {
                var mail = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private bool ValidateBankAccount(string bankAccount)
        {
            var test = Regex.IsMatch(bankAccount, "((\\d{4})-){3}\\d{4}");
            return test;
        }

    }

}
