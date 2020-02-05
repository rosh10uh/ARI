using ARI.EVOS.Common.Models;
using CSharpFunctionalExtensions;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EMP.Management.AppServices.Interface
{
    public interface IMasterData
    {
        Task<Maybe<ObservableCollection<CountryModel>>> GetCountryDetail();
    }
}
