using ARI.EVOS.Common.Models;
using CSharpFunctionalExtensions;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ARI.EVOS.Dealer.AppServices.Interface
{
    /// <summary>
    ///  This interface is used to get details of master like country and make data.
    /// </summary>
    public interface IMasterData
    {
        Task<Maybe<ObservableCollection<CountryModel>>> GetCountryDetail();

        Task<Maybe<ObservableCollection<MakeModel>>> GetMakeDetail();
    }
}
