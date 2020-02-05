using Chassis.Repository.Specification;
using System;
using System.Linq.Expressions;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;


namespace ARI.EVOS.Dealer.Infrastructure.Specification
{
    /// <summary>
    /// This class is used to set specification pattern for dealerid, country code and make code.
    /// </summary>
    public class DealerIdSpecification : Specification<Domain.Models.Dealer.Aggregate.Dealer>
    {
        private DealerId _dealerId;

        private CountryCode _countryCode;

        private MakeCode _makeCode;

        public DealerIdSpecification(CountryCode countryCode, MakeCode makeCode, DealerId dealerId)
        {
            _dealerId = dealerId;
            _countryCode = countryCode;
            _makeCode = makeCode;
        }

        public override Expression<Func<Domain.Models.Dealer.Aggregate.Dealer, bool>> ToExpression()
        {
            //return x => x.DealerId.Id.ToUpper().Trim() == _dealerId.Id.ToUpper().Trim() 
            //            && x.CountryCode.Code == _countryCode.Code 
            //            && x.MakeCode.Code == _makeCode.Code;

            return x => x.DealerId == _dealerId
                        && x.CountryCode == _countryCode
                        && x.MakeCode == _makeCode;
        }
    }
}
