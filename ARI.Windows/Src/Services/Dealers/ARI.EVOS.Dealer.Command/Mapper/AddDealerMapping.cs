using ARI.EVOS.Dealer.Command.Commands;
using ARI.EVOS.Dealers.Models;
using AutoMapper;

namespace ARI.EVOS.Dealer.Command.Mapper
{
    /// <summary>
    /// This class is used for dealer network add mapping
    /// </summary>
    public class AddDealerMapping : Profile
    {
        public AddDealerMapping()
        {
            CreateMap<DealerNetworkModel, AddDealerCommand>();
        }
    }
}
