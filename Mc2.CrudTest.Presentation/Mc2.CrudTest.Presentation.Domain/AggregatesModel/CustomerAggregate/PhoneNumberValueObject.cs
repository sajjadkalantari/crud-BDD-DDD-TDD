using Mc2.CrudTest.Presentation.Domain.Infrustructure;
using Mc2.CrudTest.Shared;
using Mc2.CrudTest.Shared.Exceptions;
using System;
using System.Collections.Generic;

namespace Mc2.CrudTest.Presentation.Domain.AggregatesModel.CustomerAggregate
{
    public class PhoneNumberValueObject: ValueObject
    {
        public PhoneNumberValueObject()
        {

        }
        public PhoneNumberValueObject(string phoneNumber)
        {
            PhoneNumber = ValidatePhonenumber(phoneNumber) ? ParsePhoneNumber(phoneNumber) : throw new CustomSystemException(Constants.ErrorCodes.InvalidMobileNumber, "invalid mobile number");
        }

        public ulong PhoneNumber { get; set; }

        private bool ValidatePhonenumber(string phonenumber)
        {
            try
            {
                if (string.IsNullOrEmpty(phonenumber)) return false;
                var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
                var phonenumberInstance = phoneNumberUtil.Parse(phonenumber, "IR");
                return phoneNumberUtil.IsValidNumber(phonenumberInstance);
            }
            catch (Exception)
            {
                return false;
            }

        }

        private ulong ParsePhoneNumber(string phonenumber)
        {
            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
            var phonenumberInstance = phoneNumberUtil.Parse(phonenumber, "IR");
            return phonenumberInstance.NationalNumber;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PhoneNumber;
        }

        public override string ToString()
        {
            return this.PhoneNumber.ToString();
        }
    }
}
