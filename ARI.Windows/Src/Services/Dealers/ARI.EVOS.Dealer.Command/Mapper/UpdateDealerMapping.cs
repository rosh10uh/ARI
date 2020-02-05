using ARI.EVOS.Dealer.Command.Commands;
using ARI.EVOS.Dealers.Models;
using AutoMapper;

namespace ARI.EVOS.Dealer.Command.Mapper
{
    /// <summary>
    /// This class is used for dealer network update dealer command to map the fields
    /// </summary>
    public class UpdateDealerMapping : Profile
    {
        public UpdateDealerMapping()
        {
            CreateMap<DealerNetworkModel, UpdateDealerCommand>();
        }
    }
}
