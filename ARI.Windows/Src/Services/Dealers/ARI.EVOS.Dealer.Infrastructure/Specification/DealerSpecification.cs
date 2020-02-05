using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Chassis.Repository.Specification;
using System;
using System.Linq.Expressions;


namespace ARI.EVOS.Dealer.Infrastructure.Specification
{
    /// <summary>
    /// This class is used to set specification pattern for dealerid, country code and make code.
    /// </summary>
    public class DealerIdSpecification : Specification<Domain.Models.Dealer.Aggregate.Dealer>
    {
        private readonly DealerId _dealerId;

        private readonly CountryCode _countryCode;

        private readonly MakeCode _makeCode;

        public DealerIdSpecification(CountryCode countryCode, MakeCode makeCode, DealerId dealerId)
        {
            _dealerId = dealerId;
            _countryCode = countryCode;
            _makeCode = makeCode;
        }

        public override Expression<Func<Domain.Models.Dealer.Aggregate.Dealer, bool>> ToExpression()
        {
            return x => x.DealerId == _dealerId
                        && x.CountryCode == _countryCode
                        && x.MakeCode == _makeCode;
        }
    }
}
