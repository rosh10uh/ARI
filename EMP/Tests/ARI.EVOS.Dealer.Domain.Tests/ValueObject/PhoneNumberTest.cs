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
        [InlineData("1234567889123456789012345")]
        [InlineData("1234567890")]
        public void To_Check_Create_PhoneNumber(string phone)
        {
            //Arrange
            var countryCode = CountryCode.Create("UK");
            Assert.ThrowsAny<Exception>(() => PhoneNumber.Create(phone, countryCode));
        }
    }
}
