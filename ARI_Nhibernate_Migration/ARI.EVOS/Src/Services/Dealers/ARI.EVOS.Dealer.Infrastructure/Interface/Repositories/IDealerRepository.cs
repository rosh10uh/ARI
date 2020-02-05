using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;

namespace ARI.EVOS.Dealer.Infrastructure.Interface.Repositories
{
    /// <summary>
    /// This interface is used to handle add, update, delete method definitions
    /// </summary>
    public interface IDealerRepository
    {
        Task AddOrUpdateDealer(Domain.Models.Dealer.Aggregate.Dealer dealer);
        
        Task DeleteDealer(Domain.Models.Dealer.Aggregate.Dealer dealer);

        Task<Maybe<Domain.Models.Dealer.Aggregate.Dealer>> GetByIdAsync(int dealerNetworkId);

        Task<Maybe<List<Domain.Models.Dealer.Aggregate.Dealer>>> GetByDealerIdAsync(CountryCode countryCode, MakeCode makeCode, DealerId dealerId);
        
        Task<IQueryable<Domain.Models.Dealer.Aggregate.Dealer>> GetAllAsync();
    }
}
