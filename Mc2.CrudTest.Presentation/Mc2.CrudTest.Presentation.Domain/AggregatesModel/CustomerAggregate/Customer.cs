using IbanNet;
using Mc2.CrudTest.Presentation.Domain.Infrustructure;
using Mc2.CrudTest.Shared;
using Mc2.CrudTest.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Mc2.CrudTest.Presentation.Domain.AggregatesModel.CustomerAggregate
{
    public class Customer : Entity, IAggregateRoot
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
            PhoneNumber = ValidatePhonenumber(phoneNumber) ? phoneNumber : throw new CustomSystemException(Constants.ErrorCodes.InvalidMobileNumber, "invalid mobile number");
            Email = ValidateEmail(email) ? email : throw new CustomSystemException(Constants.ErrorCodes.InvalidEmail, "invalid email address");
            BankAccountNumber = ValidateBankAccount(bankAccountNumber) ? bankAccountNumber : throw new CustomSystemException(Constants.ErrorCodes.InvalidBankAccount, "Invalid Bank Account Number");
        }

        public Customer(int id, string firstname, string lastname, DateTime dateOfBirth, string phoneNumber, string email, string bankAccountNumber) : this(firstname, lastname, dateOfBirth, phoneNumber, email, bankAccountNumber)
        {
            Id = id;            
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
            IIbanValidator validator = new IbanValidator();
            ValidationResult validationResult = validator.Validate(bankAccount);
            return validationResult.IsValid;
        }

    }
}
