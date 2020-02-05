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
        Fax fax;
        Email email;

        [Theory]
        [InlineData("1234569874563214587965231", "UK")]
        [InlineData("7896641365", "UK")]
        public void To_Check_Create(string phone, string code)
        {
            //Arrange
            var countryCode = CountryCode.Create(code);
            Assert.ThrowsAny<Exception>(() => PhoneNumber.Create(phone, countryCode));
            fax = Fax.Create("1234569874563214587965231", countryCode);
            email = Email.Create("email@email.com");

            //Act
            var contactInformation = ContactInformation.Create("Test1", "Test2", phoneNumber, phoneNumber, "Test3", "Test4", fax, email);

            //Assert
            Assert.IsType<ContactInformation>(contactInformation);
        }

        [Theory]
        [InlineData("1234569874563214587965231", "UK")]
        [InlineData("7896641365", "UK")]
        public void To_Check_Update(string phone, string code)
        {
            //Arrange   
            var countryCode = CountryCode.Create(code);
            Assert.ThrowsAny<Exception>(() => PhoneNumber.Create(phone, countryCode));
            fax = Fax.Create("1234569874563214587965231", countryCode);
            email = Email.Create("email@email.com");

            //Act
            var contactInformation = ContactInformation.Create("Test1", "Test2", phoneNumber, phoneNumber, "Test3", "Test4", fax, email);

            contactInformation.Update("Test11", "Test23", phoneNumber, phoneNumber, "Test5", "Test7", fax, email);
        }
    }
}
