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
            var email = Email.Create("jagruti@gmail.com");

            Assert.IsType<Email>(email);
        }
    }
}
