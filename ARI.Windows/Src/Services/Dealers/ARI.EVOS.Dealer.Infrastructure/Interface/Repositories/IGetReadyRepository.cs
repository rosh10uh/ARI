using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARI.EVOS.Dealer.Infrastructure.Interface.Repositories
{
    /// <summary>
    /// This interface is used to get access to Get Ready repository.
    /// </summary>
    public interface IGetReadyRepository
    {
        Task AddOrUpdateGetReady(GetReady getReady);
        Task<IQueryable<GetReady>> GetAllAsync();
        Task<Maybe<List<GetReady>>> GetByGetReadyVehicleAsync(CountryCode countryCode, MakeCode makeCode, DealerId dealerId,string vehical);
        Task<Maybe<GetReady>> GetByIdAsync(int getReadyId);
        Task DeleteGetReadyDetail(GetReady getReady);
    }
}
