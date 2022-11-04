using Mc2.CrudTest.Presentation.Domain.Infrustructure;
using Mc2.CrudTest.Shared;
using Mc2.CrudTest.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace Mc2.CrudTest.Presentation.Domain.AggregatesModel.CustomerAggregate
{
    public class EmailValueObject : ValueObject
    {
        public EmailValueObject()
        {

        }
        public EmailValueObject(string email)
        {
            Email = ValidateEmail(email) ? email.ToLower() : throw new CustomSystemException(Constants.ErrorCodes.InvalidEmail, "invalid email address");
        }

        public string Email { get; set; }

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

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Email;
        }
        public override string ToString()
        {
            return this.Email.ToString();
        }
    }
}
