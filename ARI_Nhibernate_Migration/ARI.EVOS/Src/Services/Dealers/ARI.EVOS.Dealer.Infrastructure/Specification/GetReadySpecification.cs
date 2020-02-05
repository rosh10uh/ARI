
using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Chassis.Repository.Specification;
using System;
using System.Linq.Expressions;

namespace ARI.EVOS.Dealer.Infrastructure.Specification
{
    /// <summary>
    ///  This class is used to set specification pattern for vehicle base get ready detail
    /// </summary>
    public class GetReadySpecification : Specification<GetReady>
    {
        private string _grVehical;
        public string GrVehicals
        {
            get => _grVehical;
            set => _grVehical = value;
        }
        public override Expression<Func<GetReady, bool>> ToExpression()
        {
            return emp1 =>  emp1.GRVehicles== _grVehical;
        }
    }
    /// <summary>
    ///  Specification for dealer id base get ready detail
    /// </summary>
    public class GetReadyDealerIdSpecification : Specification<GetReady>
    {
        private DealerId _dealerId;
        public DealerId DealerId
        {
            get => _dealerId;
            set => _dealerId = value;
        }

        public override Expression<Func<GetReady, bool>> ToExpression()
        {
            return emp1 => emp1.DealerId == _dealerId ;
        }
    }

    /// <summary>
    ///  Specification for country code base get ready detail
    /// </summary>
    public class GetReadyCountryCodeSpecification : Specification<GetReady>
    {
        
        private CountryCode _countryCode;
        public CountryCode CountryCode
        {
            get => _countryCode;
            set => _countryCode = value;
        }

      
        public override Expression<Func<GetReady, bool>> ToExpression()
        {
            return emp1 => emp1.CountryCode == _countryCode;
        }
    }

    /// <summary>
    ///  Specification for make code base get ready detail
    /// </summary>
    public class GetReadyMakeCodeSpecification : Specification<GetReady>
    {
        
        private MakeCode _makeCode;
        public MakeCode MakeCode
        {
            get => _makeCode;
            set => _makeCode = value;
        }
       
        public override Expression<Func<GetReady, bool>> ToExpression()
        {
            return emp1 =>  emp1.MakeCode == _makeCode;
        }
    }
}
