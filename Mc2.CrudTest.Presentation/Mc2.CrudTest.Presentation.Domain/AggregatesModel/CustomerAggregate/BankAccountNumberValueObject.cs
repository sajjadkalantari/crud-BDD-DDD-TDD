using IbanNet;
using Mc2.CrudTest.Presentation.Domain.Infrustructure;
using Mc2.CrudTest.Shared;
using Mc2.CrudTest.Shared.Exceptions;
using System.Collections.Generic;

namespace Mc2.CrudTest.Presentation.Domain.AggregatesModel.CustomerAggregate
{
    public class BankAccountNumberValueObject : ValueObject
    {
        public BankAccountNumberValueObject()
        {

        }
        public BankAccountNumberValueObject(string bankAccountNumber)
        {
            BankAccountNumber = ValidateBankAccount(bankAccountNumber) ? bankAccountNumber : throw new CustomSystemException(Constants.ErrorCodes.InvalidBankAccount, "Invalid Bank Account Number");
        }
        public string BankAccountNumber { get; set; }


        private bool ValidateBankAccount(string bankAccount)
        {
            IIbanValidator validator = new IbanValidator();
            ValidationResult validationResult = validator.Validate(bankAccount);
            return validationResult.IsValid;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return BankAccountNumber;
        }

        public override string ToString()
        {
            return this.BankAccountNumber.ToString();
        }

    }
}
