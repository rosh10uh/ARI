using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Xunit;

namespace ARI.EVOS.Dealer.Domain.Tests.Dealer.ValueObject
{
    /// <summary>
    /// Test cases of user value object
    /// </summary>
    public class UserTest
    {
        [Fact]
        public void To_Check_Create_User()
        {
            //Arrange
            var user = User.Create("Jack");

            Assert.IsType<User>(user);
        }
    }
}
