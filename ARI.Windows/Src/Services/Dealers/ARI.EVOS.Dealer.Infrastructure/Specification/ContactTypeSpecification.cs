using ARI.EVOS.Dealer.Domain.SharedKernel;
using Chassis.Repository.Specification;
using System;
using System.Linq.Expressions;
using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;

namespace ARI.EVOS.Dealer.Infrastructure.Specification
{
    /// <summary>
    /// This class is used to set specification pattern for contact type Stock,Finance and License
    /// </summary>
    public class ContactTypeSpecification : Specification<ContactEmail>
    {
        private readonly ContactType _contactType;
        public ContactTypeSpecification(ContactType contactType)
        {
            _contactType = contactType;
        }
        public override Expression<Func<ContactEmail, bool>> ToExpression()
        {
            return contact => contact.ContactType == _contactType;
        }
    }
}
