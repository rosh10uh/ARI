using ARI.EVOS.Dealer.AppServices.Interface;
using ARI.EVOS.Dealer.Command.Commands;
using ARI.EVOS.Dealer.Query.Queries;
using ARI.EVOS.Dealers.Models;
using Chassis.Command.Interfaces;
using Chassis.Dapper.Interfaces.Query;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ARI.EVOS.Dealer.AppServices
{
    /// <summary>
    /// This interface is used to handle add, update, delete method implementation through command bus
    /// </summary>
    public class DealerNetwork : BaseAppService, IDealerNetwork
    {
        public DealerNetwork(ICommandBus commandBus, IQueryDispatcher queryDispatcher) : base(commandBus, queryDispatcher)
        {
        }

        /// <summary>
        /// This method is used to map dealer network model with dealer network to pass command handler
        /// </summary>
        /// <param name="dealerNetworkModel"></param>
        /// <returns></returns>
        public Task<Result<string>> InsertDealerNetwork(DealerNetworkModel dealerNetworkModel)
        {
            return DispatchCommand<AddDealerCommand, string>(dealerNetworkModel);
        }

        /// <summary>
        /// This method is used to map dealer network model with dealer network to pass command handler
        /// </summary>
        /// <param name="dealerNetworkModel"></param>
        /// <returns></returns>
        public Task<Result<string>> UpdateDealerNetwork(DealerNetworkModel dealerNetworkModel)
        {
            return DispatchCommand<UpdateDealerCommand, string>(dealerNetworkModel);
        }

        /// <summary>
        /// This method is used to map dealer network model with dealer network to delete dealer, passing through command bus
        /// </summary>
        /// <param name="dealerNetworkModel"></param>
        /// <returns></returns>
        public Task<Result<string>> DeleteDealerNetwork(DealerNetworkModel dealerNetworkModel)
        {
            return DispatchCommand<DeleteDealerCommand, string>(dealerNetworkModel);
        }

        /// <summary>
        /// This method is used to map Get Ready model to pass insert command handler
        /// </summary>
        /// <param name="GetReadyModel"></param>
        /// <returns>Task<Result<string>></returns>
        public Task<Result<string>> InsertGetReady(GetReadyModel getReady)
        {
            return DispatchCommand<AddGetReadyCommand, string>(getReady);
        }
        /// <summary>
        /// This method is used to map Get Ready model to pass update command handler
        /// </summary>
        /// <param name="GetReadyModel"></param>
        /// <returns>Task<Result<string>></returns>
        public Task<Result<string>> UpdateGetReady(GetReadyModel getReady)
        {
            return DispatchCommand<UpdateGetReadyCommand, string>(getReady);
        }

        /// <summary>
        /// This method is used to map Get Ready model to pass delete command handler
        /// </summary>
        /// <param name="GetReadyModel"></param>
        /// <returns>Task<Result<string>></returns>
        public Task<Result<string>> DeleteGetReady(GetReadyModel getReady)
        {
            return DispatchCommand<DeleteGetReadyCommand, string>(getReady);
        }

        /// <summary>
        /// This method is used to map Contact Email model to save email contact
        /// </summary>
        /// <param name="additionalEmailModel"></param>
        /// <returns></returns>
        public Task<Result<string>> SaveEmail(ContactEmailModel contactEmailModel)
        {
            return DispatchCommand<ContactEmailCommand, string>(contactEmailModel);
        }


        /// <summary>
        /// This method is used to map Contact Email model to get email details based on contact type
        /// </summary>
        /// <returns>AdditionalEmailModel</returns>
        public Task<Maybe<ObservableCollection<ContactEmailModel>>> GetContactEmailDetail(string countryCode, string makeCode, string dealerId)
        {
            return DispatchQuery(new GetContactEmailDetailQuery(countryCode, makeCode, dealerId));
        }

        /// <summary>
        /// This method is used to map Get dealer's detail based on passed dealerId and make code and country id
        /// </summary>
        /// <param name="dealerModel"></param>
        public Task<Maybe<IEnumerable<DealerNetworkModel>>> GetDealerNetwork(DealerNetworkModel dealerNetworkModel)
        {
            return DispatchQuery(new GetDealerQuery(dealerNetworkModel.CountryCode, dealerNetworkModel.MakeCode, dealerNetworkModel.DealerId));

        }

        /// <summary>
        /// This method is used to map Get user's detail based on passed user Id Parameter 
        /// </summary>
        public Task<Maybe<string>> GetUserSecurityCode(string userId)
        {
            return DispatchQuery(new GetUserSecurityCodeQuery(userId));
        }

        /// <summary>
        /// This method is used to map Dealer Search model to get dealers by search criteria
        /// </summary>
        /// <param name="dealersSearchModel"></param>
        /// <returns></returns>
        public Task<Maybe<ObservableCollection<DealerSearchModel>>> SearchDealers(DealerSearchModel dealersSearchModel)
        {
            return DispatchQuery(new SearchDealersQuery(dealersSearchModel.CountryCode, dealersSearchModel.MakeCode, dealersSearchModel.DealerId, dealersSearchModel.VendorName));
        }

        /// <summary>
        /// This method is used to map Dealer Search model to get dealers list
        /// </summary>
        /// <returns></returns>
        public Task<Maybe<ObservableCollection<DealerSearchModel>>> GetDealersList()
        {
            return DispatchQuery(new GetDealersListQuery());
        }

        /// <summary>
        /// This method is used to map Get city based on zipcode. 
        /// </summary>
        public Task<Maybe<DealerNetworkModel>> GetCityFromZip(string zipCode)
        {
            return DispatchQuery(new GetCityFromZipQuery(zipCode));
        }

        /// <summary>
        /// This method is used to map Get dealer base get ready details
        /// </summary>
        /// <param name="getReady"></param>
        /// <returns></returns>
        public Task<Maybe<ObservableCollection<GetReadyModel>>> GetReadyDetails(GetReadyModel getReady)
        {
            return DispatchQuery(new GetReadyDetails(getReady.CountryCode, getReady.MakeCode, getReady.DealerId));
        }
    }
}
