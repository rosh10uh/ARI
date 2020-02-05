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
            var userMock = new UserMock();
            var user = User.Create("Jack");
            userMock.Equals(user);
            Assert.IsType<User>(user);
        }
    }

    public class UserMock : User
    {
        public UserMock()
        {

        }
        public bool Equals(User other)
        {
            return base.EqualsCore(other);
        }
    }
}
