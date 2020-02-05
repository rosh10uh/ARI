using ARI.EVOS.Dealer.Command.Commands;
using ARI.EVOS.Dealers.Models;
using AutoMapper;

namespace ARI.EVOS.Dealer.Command.Mapper
{
    /// <summary>
    /// This class is used for get ready add command to map the fields
    /// </summary>
    public class AddGetReadyMappling : Profile
    {
        public AddGetReadyMappling()
        {
            CreateMap<GetReadyModel, AddGetReadyCommand>();           
        }
    }
}
