using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.ValueObject
{
    /// <summary>
    /// Test cases of Email value object
    /// </summary>
    public class EmailTest
    {
        [Fact]
        public void To_Check_Create_Email()
        {
            //Arrange
            var emailMock = new EmailMock();
            var email = Email.Create("jagruti@gmail.com");
            Email.Create("");
            emailMock.Equals(email);
            Assert.IsType<Email>(email);
        }
    }

    public class EmailMock : Email
    {
        public EmailMock()
        {

        }
        public bool Equals(Email other)
        {
            return base.EqualsCore(other);
        }
    }
}
