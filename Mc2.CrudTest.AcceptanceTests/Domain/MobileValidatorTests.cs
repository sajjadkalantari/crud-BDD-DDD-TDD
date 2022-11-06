using Mc2.CrudTest.Presentation.Domain.Validators;
using Xunit;

namespace Mc2.CrudTest.AcceptanceTests.Domain
{
    public class MobileValidatorTests
    {
        [Theory]
        [InlineData("+989121234567", true)]
        [InlineData("+31612345678", true)]
        [InlineData("+982188776655", false)]
        public void MobileValidatorTest_WithExpectedResult(string phoneNumber, bool expectedResult)
        {
            bool testResult = MobileValidator.IsValid(phoneNumber);
            
            Assert.Equal(expectedResult, testResult);
        }
    }
}