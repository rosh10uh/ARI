using ARI.EVOS.Dealers.Models;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ARI.EVOS.Dealer.AppServices.Interface
{
    /// <summary>
    /// This interface is used to handle add, update, delete method definitions through command bus
    /// </summary>
    public interface IDealerNetwork
    {
        Task<Result<string>> InsertDealerNetwork(DealerNetworkModel dealerNetworkModel);
        Task<Result<string>> DeleteDealerNetwork(DealerNetworkModel dealerNetworkModel);
        Task<Result<string>> UpdateDealerNetwork(DealerNetworkModel dealerNetworkModel);

        Task<Result<string>> InsertGetReady(GetReadyModel getReady);
        Task<Result<string>> UpdateGetReady(GetReadyModel getReady);
        Task<Result<string>> DeleteGetReady(GetReadyModel getReady);

        Task<Result<string>> SaveEmail(ContactEmailModel contactEmailModel);

        Task<Maybe<ObservableCollection<ContactEmailModel>>> GetContactEmailDetail(string countryCode, string makeCode, string dealerId);

        Task<Maybe<IEnumerable<DealerNetworkModel>>> GetDealerNetwork(DealerNetworkModel dealerNetworkModel);

        Task<Maybe<string>> GetUserSecurityCode(string userId);

        Task<Maybe<ObservableCollection<DealerSearchModel>>> SearchDealers(DealerSearchModel dealersSearchModel);

        Task<Maybe<ObservableCollection<DealerSearchModel>>> GetDealersList();

        Task<Maybe<DealerNetworkModel>> GetCityFromZip(string zipCode);

        Task<Maybe<ObservableCollection<GetReadyModel>>> GetReadyDetails(GetReadyModel getReady);
    }
}
