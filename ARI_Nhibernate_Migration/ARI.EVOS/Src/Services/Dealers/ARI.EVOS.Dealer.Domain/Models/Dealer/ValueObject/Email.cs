using System;
using System.Text.RegularExpressions;
using Chassis.Domain.Aggregate;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject
{
    /// <summary>
    /// This class is used to store email value
    /// </summary>
    public class Email : ValueObject<Email>
    {
        public virtual string EmailAddress { get; protected set; }

        protected Email()
        {
        }

        private Email(string emailAddress)
        {
            EmailAddress = emailAddress;
        }

        public static Email Create(string emailAddress)
        {
            var email = new Email(emailAddress);
            return email.IsValid() ? email : throw new Exception(DealerDomainConstant.EmailValidateErrorMessage);
        }
        
        private bool IsValid()
        {
            if (!string.IsNullOrEmpty(EmailAddress))
            {
                EmailAddress = Regex.Replace(EmailAddress, @"\s", "");
                Match match = Regex.Match(EmailAddress, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                return match.Success;
            }
            return true;
        }
        protected override bool EqualsCore(Email email)
        {
            return EmailAddress == email.EmailAddress;
        }

        protected override int GetHashCodeCore()
        {
            return base.GetHashCode();
        }
    }
}
