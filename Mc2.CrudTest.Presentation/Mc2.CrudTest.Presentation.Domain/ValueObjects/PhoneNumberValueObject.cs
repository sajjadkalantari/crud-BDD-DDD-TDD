using Mc2.CrudTest.Presentation.Domain.Infrustructure;
using Mc2.CrudTest.Shared;
using Mc2.CrudTest.Shared.Exceptions;
using System;
using System.Collections.Generic;
using PhoneNumbers;

namespace Mc2.CrudTest.Presentation.Domain.AggregatesModel.CustomerAggregate
{
    public class PhoneNumberValueObject: ValueObject
    {
        public PhoneNumberValueObject(ulong phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public PhoneNumberValueObject(string phoneNumber)
        {
            PhoneNumber = ulong.Parse(phoneNumber.Trim('+').Replace(" ", ""));
            PhoneNumber = ValidatePhonenumber($"+{phoneNumber}") ? ParsePhoneNumber(phoneNumber) : throw new CustomSystemException(Constants.ErrorCodes.InvalidMobileNumber, "invalid mobile number");
        }

        public ulong PhoneNumber { get; set; }

        private static readonly PhoneNumberUtil PhoneNumberUtil = PhoneNumberUtil.GetInstance();

        private bool ValidatePhonenumber(string phonenumber)
        {
            try
            {
                if (string.IsNullOrEmpty(phonenumber)) return false;
                var phonenumberInstance = PhoneNumberUtil.Parse(phonenumber, "IR");
                return PhoneNumberUtil.IsValidNumber(phonenumberInstance);
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
            return $"+{PhoneNumber.ToString()}";
        }
    }
}
