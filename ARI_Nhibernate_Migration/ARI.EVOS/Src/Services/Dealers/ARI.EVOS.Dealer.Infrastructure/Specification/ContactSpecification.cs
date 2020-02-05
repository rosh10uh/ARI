using Chassis.Repository.Specification;
using System;
using System.Linq.Expressions;
using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;

namespace ARI.EVOS.Dealer.Infrastructure.Specification
{
    /// <summary>
    /// This class is used to set specification pattern for contact Dealer Id,Country code and make code
    /// </summary>
    public class ContactSpecification : Specification<ContactEmail>
    {
        private readonly DealerId _dealerId;
        private readonly CountryCode _countryCode;
        private readonly MakeCode _makeCode;
        public ContactSpecification(DealerId dealerId, CountryCode countryCode, MakeCode makeCode)
        {
            _dealerId = dealerId;
            _countryCode = countryCode;
            _makeCode = makeCode;
        }
        public override Expression<Func<ContactEmail, bool>> ToExpression()
        {
            //return contact => contact.DealerId.Id == _dealerId.Id && contact.CountryCode.Code == _countryCode.Code && contact.MakeCode.Code == _makeCode.Code;
            return contact => contact.DealerId == _dealerId && contact.CountryCode == _countryCode && contact.MakeCode == _makeCode;
        }
    }
}
