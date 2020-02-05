using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using ARI.EVOS.Dealer.Domain.SharedKernel;
using ARI.EVOS.Dealer.Infrastructure.Interface.Repositories;
using ARI.EVOS.Dealer.Infrastructure.Specification;
using Chassis.Repository;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ARI.EVOS.Dealer.Infrastructure.Repositories
{
    /// <summary>
    /// This repository is used to handle add,update,delete email functionality
    /// </summary>
    public class ContactEmailRepository : BaseRepository<ContactEmail>, IContactEmailRepository
    {
        public ContactEmailRepository(IContext context) : base(context)
        {
        }

        /// <summary>
        ///  Get contact Detail based on given CountryCode,MakeCode,DealerId,ContactType.
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="makeCode"></param>
        /// <param name="dealerId"></param>
        /// <param name="contactType"></param>
        public async Task<Maybe<List<ContactEmail>>> CheckContactEmail(CountryCode countryCode, MakeCode makeCode, DealerId dealerId, ContactType contactType)
        {
            // Set fetch custom criteria using specification 
            var contactSpecification = new ContactSpecification(dealerId, countryCode, makeCode);
            var contactTypeSpecification = new ContactTypeSpecification(contactType);

            // Get Contact Detail using specified criteria in specification 
            return await base.GetAllAsync(contactSpecification.And(contactTypeSpecification));
        }

        /// <summary>
        /// This method is used to add or update contact detail.
        /// </summary>
        /// <param name="contactEmail"></param>
        /// <returns></returns>
        public async Task AddOrUpdateEmail(ContactEmail contactEmail)
        {
            await base.SaveOrUpdateAsync(contactEmail);
        }
    }
}
