using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using System;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.ValueObject
{
    /// <summary>
    /// Test cases of Phone number value object
    /// </summary>
    public class PhoneNumberTest
    {
        [Theory]
        [InlineData("12345698745632145879652311", "UK")]
        [InlineData("12345678901", "CA")]
        public void To_Check_Create_PhoneNumber(string phone, string code)
        {
            //Arrange
            var countryCode = CountryCode.Create(code);
            Assert.ThrowsAny<Exception>(() => PhoneNumber.Create(phone, countryCode));
        }
    }
}
