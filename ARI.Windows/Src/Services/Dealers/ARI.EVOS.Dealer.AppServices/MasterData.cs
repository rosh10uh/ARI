using ARI.EVOS.Dealer.AppServices.Interface;
using ARI.EVOS.Dealer.Query.Queries;
using CSharpFunctionalExtensions;
using System.Threading.Tasks;
using ARI.EVOS.Common.Models;
using System.Collections.ObjectModel;
using Chassis.Command.Interfaces;
using Chassis.Dapper.Interfaces.Query;

namespace ARI.EVOS.Dealer.AppServices
{
    /// <summary>
    /// This class is used to handle master data like country and make details through command bus
    /// </summary>
    public class MasterData : BaseAppService, IMasterData
    {        
        public MasterData(ICommandBus commandBus, IQueryDispatcher queryDispatcher) : base(commandBus, queryDispatcher)
        {            
        }

        /// <summary>
        ///  Get country detail 
        /// </summary>
        public Task<Maybe<ObservableCollection<CountryModel>>> GetCountryDetail()
        {
            return QueryDispatcher.DispatchAsync(new GetCountryDetailQuery());
        }

        /// <summary>
        ///  Get make detail
        /// </summary>       
        public Task<Maybe<ObservableCollection<MakeModel>>> GetMakeDetail()
        {
            return QueryDispatcher.DispatchAsync(new GetMakeDetailQuery());
        }

    }
}
