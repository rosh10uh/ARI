using ARI.EVOS.Dealer.Command.Commands;
using ARI.EVOS.Dealers.Models;
using AutoMapper;

namespace ARI.EVOS.Dealer.Command.Mapper
{
    /// <summary>
    /// This class is used for dealer delete command to map the fields
    /// </summary>
    public class DeleteDealerMapping : Profile
    {
        public DeleteDealerMapping()
        {
            CreateMap<DealerNetworkModel, DeleteDealerCommand>();
        }
    }
}
