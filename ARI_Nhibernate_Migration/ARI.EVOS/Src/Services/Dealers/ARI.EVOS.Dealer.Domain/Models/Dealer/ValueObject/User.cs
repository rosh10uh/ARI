using Chassis.Domain.Aggregate;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject
{
    /// <summary>
    /// This class is used to store user value
    /// </summary>
    public class User : ValueObject<User>
    {
        public virtual string UserName { get; protected set; }

        protected User()
        {
        }

        private User(string userName)
        {
            UserName = userName;
        }

        public static User Create(string userName)
        {
            return new User(userName);
        }
        protected override bool EqualsCore(User user)
        {
            return UserName == user.UserName;
        }

        protected override int GetHashCodeCore()
        {
            return base.GetHashCode();
        }
    }
}
