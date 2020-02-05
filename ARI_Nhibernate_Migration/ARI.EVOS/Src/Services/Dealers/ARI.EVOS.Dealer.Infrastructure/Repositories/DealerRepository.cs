using ARI.EVOS.Dealer.Infrastructure.Interface.Repositories;
using ARI.EVOS.Dealer.Infrastructure.Specification;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Chassis.Domain;
using Chassis.Repository;

namespace ARI.EVOS.Dealer.Infrastructure.Repositories
{
    /// <summary>
    /// This repository is used to handle add,update,delete delete functionalities
    /// </summary>
    public class DealerRepository : DomainRepository<Domain.Models.Dealer.Aggregate.Dealer,int>, IDealerRepository
    {
        public DealerRepository(IContext context) : base(context)
        {
        }

        public Task<Maybe<Domain.Models.Dealer.Aggregate.Dealer>> GetByIdAsync(int dealerNetworkId)
        {
            return base.GetByIdAsync(dealerNetworkId);
        }
       
        /// <summary>
        ///  Get Dealer's Detail based on given Dealer Id.
        /// </summary>
        /// <param name="dealerId"></param>
        public Task<Maybe<List<Domain.Models.Dealer.Aggregate.Dealer>>> GetByDealerIdAsync(CountryCode countryCode, MakeCode makeCode, DealerId dealerId)
        {
            // Set fetch custom criteria using specification 
            var dealerSpecification = new DealerIdSpecification(countryCode, makeCode, dealerId);
            
            // Get Dealer Detail using specified criteria in specification 
            return base.GetAllAsync(dealerSpecification);
        }

        public async Task AddOrUpdateDealer(Domain.Models.Dealer.Aggregate.Dealer dealer)
        {
            await base.SaveOrUpdateAsync(dealer);
        }

        public override async Task<IQueryable<Domain.Models.Dealer.Aggregate.Dealer>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task DeleteDealer(Domain.Models.Dealer.Aggregate.Dealer dealer)
        {
            await base.DeleteAsync(dealer);
        }
    }
}
