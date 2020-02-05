using ARI.EVOS.Dealer.Domain.SharedKernel;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;

namespace ARI.EVOS.Dealer.Infrastructure.Interface.Repositories
{
    /// <summary>
    /// This interface is used to handle add, update methods of contact email definitions
    /// </summary>
    public interface IContactEmailRepository
    {
        Task<Maybe<List<ContactEmail>>> CheckContactEmail(CountryCode countryCode, MakeCode makeCode, DealerId dealerId,ContactType contactType);
        Task<IQueryable<ContactEmail>> GetAllAsync();
        Task AddOrUpdateEmail(ContactEmail additionalEmail);
    }
}
