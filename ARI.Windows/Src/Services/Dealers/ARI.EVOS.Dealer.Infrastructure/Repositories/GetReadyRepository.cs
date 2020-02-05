using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using ARI.EVOS.Dealer.Infrastructure.Interface.Repositories;
using ARI.EVOS.Dealer.Infrastructure.Specification;
using Chassis.Repository;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ARI.EVOS.Dealer.Infrastructure.Repositories
{
    /// <summary>
    /// This repository is used to handle add,update,delete Get Ready functionalists
    /// </summary>
    public class GetReadyRepository : BaseRepository<GetReady>, IGetReadyRepository
    {
        public GetReadyRepository(IContext context) : base(context)
        {
        }

        /// <summary>
        /// Add or update get reday details
        /// </summary>
        /// <param name="getReady"></param>
        /// <returns>Task</returns>
        public virtual async Task AddOrUpdateGetReady(GetReady getReady)
        {
            await base.SaveOrUpdateAsync(getReady);
        }

        /// <summary>
        /// Get All get ready details
        /// </summary>
        /// <returns>Task<Maybe<IQueryable<Domain.Models.GetReady>>></returns>
        public async override Task<IQueryable<GetReady>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        /// <summary>
        /// Get id base get ready detail
        /// </summary>
        /// <param name="getReadyId"></param>
        /// <returns>Task<Maybe<GetReady>></returns>
        public Task<Maybe<GetReady>> GetByIdAsync(int getReadyId)
        {
            return base.GetByIdAsync(getReadyId);
        }

        /// <summary>
        ///  Get Dealer and Vehicle base get detail
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="makeCode"></param>
        /// <param name="dealerId"></param>
        /// <param name="vehical"></param>
        /// <returns>Task<Maybe<List<Domain.Models.GetReady>>></returns>
        public async Task<Maybe<List<GetReady>>> GetByGetReadyVehicleAsync(CountryCode countryCode, MakeCode makeCode, DealerId dealerId, string vehical)
        {
            // Set fetch custom criteria using specification 
            var getReadyCountryCodeSpecification = new GetReadyCountryCodeSpecification
            {
                CountryCode = countryCode               
            };

            var getReadyMakeCodeSpecification = new GetReadyMakeCodeSpecification
            {
                MakeCode = makeCode               
            };

            var getReadyDealerIdSpecification = new GetReadyDealerIdSpecification
            {               
                DealerId = dealerId                
            };

            var getReadyGRVehicalSpecification = new GetReadySpecification
            {
                 GrVehicals = vehical
            };

            // Fetch get ready detail using specified criteria in specification 
             return await base.GetAllAsync(getReadyCountryCodeSpecification.And(getReadyMakeCodeSpecification.And(getReadyDealerIdSpecification.And(getReadyGRVehicalSpecification))));
        }

        /// <summary>
        /// Delete Get Ready Detail based on value of getReadyId.
        /// </summary>
        /// <param name="getReadyId"></param>
        /// <returns>Task</returns>
        public async Task DeleteGetReadyDetail(GetReady getReady)
        {
            // Call DBContext method for Delete Get Ready's Detail
            var result = await base.GetByIdAsync(getReady);
            await base.DeleteAsync(result.Value);
        }
    }
}
