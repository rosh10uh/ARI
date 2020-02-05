using ARI.EVOS.Common.Models;
using ARI.EVOS.Dealer.AppServices;
using Chassis.Command.Interfaces;
using Chassis.Dapper.Interfaces.Query;
using CSharpFunctionalExtensions;
using EMP.Management.AppServices.Interface;
using EMP.Management.Query.Queries;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EMP.Management.AppServices
{
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
    }
}
