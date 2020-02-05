using ARI.EVOS.Dealer.Command.Commands;
using ARI.EVOS.Dealers.Models;
using AutoMapper;

namespace ARI.EVOS.Dealer.Command.Mapper
{
    /// <summary>
    /// This class is used for get ready update get ready command to map the fields
    /// </summary>
    public class UpdateGetReadyMappling : Profile
    {
        public UpdateGetReadyMappling()
        {
            CreateMap<GetReadyModel, UpdateGetReadyCommand>();           
        }
    }
}
