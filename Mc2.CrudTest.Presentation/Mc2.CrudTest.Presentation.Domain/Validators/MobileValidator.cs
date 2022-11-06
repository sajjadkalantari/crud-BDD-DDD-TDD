using System;
using PhoneNumbers;

namespace Mc2.CrudTest.Presentation.Domain.Validators
{
    public static class MobileValidator
    {
        private static readonly PhoneNumberUtil PhoneNumberUtil = PhoneNumberUtil.GetInstance();
        
        public static bool IsValid(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return false;
            
            try
            {
                PhoneNumber phoneNumberInstance = PhoneNumberUtil.Parse(phoneNumber, null);
                return PhoneNumberUtil.IsValidNumber(phoneNumberInstance);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}