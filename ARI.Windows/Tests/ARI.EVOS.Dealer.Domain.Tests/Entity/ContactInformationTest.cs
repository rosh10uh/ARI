using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using System;
using Xunit;
namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.Entity
{
    /// <summary>
    /// Test cases of Contact information Entity
    /// </summary>
    public class ContactInformationTest
    {
        PhoneNumber phoneNumber;
        Email email;

        [Theory]
        [InlineData("12345698745632145879652312", "UK", "1234569874563214587965231")]
        [InlineData("789664136512", "CA", "12345678901")]
        public void To_Check_Create(string phone, string code, string faxNumber)
        {
            //Arrange
            var countryCode = CountryCode.Create(code);
            Assert.ThrowsAny<Exception>(() => PhoneNumber.Create(phone, countryCode));
            var fax = Fax.Create(faxNumber, countryCode);
            email = Email.Create("email@email.com");

            //Act
            var contactInformation = ContactInformation.Create("Test1", "Test2", phoneNumber, phoneNumber, "Test3", "Test4");
            contactInformation.SetOtherContactInformation(fax, email);

            //Assert
            Assert.IsType<ContactInformation>(contactInformation);
        }

        [Theory]
        [InlineData("12345698745632145879652311", "UK", "1234569874563214587965231")]
        [InlineData("7896641365", "CA", "12345678901")]
        public void To_Check_Update(string phone, string code, string faxNumber)
        {
            //Arrange   
            var countryCode = CountryCode.Create(code);
            Assert.ThrowsAny<Exception>(() => PhoneNumber.Create(phone, countryCode));
            var fax = Fax.Create(faxNumber, countryCode);
            email = Email.Create("email@email.com");

            //Act
            var contactInformation = ContactInformation.Create("Test1", "Test2", phoneNumber, phoneNumber, "Test3", "Test4");
            contactInformation.SetOtherContactInformation(fax, email);
            contactInformation.Update("Test11", "Test23", phoneNumber, phoneNumber, "Test5", "Test7");
        }
    }
}
