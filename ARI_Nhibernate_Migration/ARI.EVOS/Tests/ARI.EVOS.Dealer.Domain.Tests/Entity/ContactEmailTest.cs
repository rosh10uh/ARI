using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using ARI.EVOS.Dealer.Domain.SharedKernel;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.Entity
{
    /// <summary>
    /// Test cases of Contact Email Entity
    /// </summary>
    public class ContactEmailTest
    {
        CountryCode countryCode;
        MakeCode makeCode;
        DealerId dealerId;
        ContactType contactType;
        Email email;

        private static void SetContactEmail(out CountryCode countryCode, out MakeCode makeCode, out DealerId dealerId, out ContactType contactType, out Email email)
        {
            countryCode = CountryCode.Create("Test1");
            makeCode = MakeCode.Create("Test1", countryCode);
            dealerId = DealerId.Create("1");
            contactType =ContactType.Finance;
            email = Email.Create("email@email.com");
        }

        [Fact]
        public void To_Check_Create()
        {
            //Arrange
            SetContactEmail(out countryCode, out makeCode, out dealerId, out contactType, out email);

            //Act
            var contactEmail = ContactEmail.Create(countryCode, makeCode, dealerId, contactType, "Test", email);

            //Assert
            Assert.IsType<ContactEmail>(contactEmail);
        }

        [Fact]
        public void To_Check_Save_Additional_Email()
        {
            //Arrange
            SetContactEmail(out countryCode, out makeCode, out dealerId, out contactType, out email);

            //Act
            var contactEmail = ContactEmail.Create(countryCode, makeCode, dealerId, contactType, "Test", email);

            contactEmail.SaveAdditionalEmail(contactEmail);
        }

        [Fact]
        public void To_Check_Save_Update_Email()
        {
            //Arrange
            SetContactEmail(out countryCode, out makeCode, out dealerId, out contactType, out email);

            //Act
            var contactEmail = ContactEmail.Create(countryCode, makeCode, dealerId, contactType, "Test", email);

            contactEmail.SaveUpdateEmail(contactEmail, "name", email);
        }
    }
}
