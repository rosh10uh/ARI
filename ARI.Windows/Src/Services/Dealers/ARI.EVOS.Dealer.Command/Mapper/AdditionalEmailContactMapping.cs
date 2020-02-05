using ARI.EVOS.Dealer.Command.Commands;
using ARI.EVOS.Dealers.Models;
using AutoMapper;

namespace ARI.EVOS.Dealer.Command.Mapper
{
    /// <summary>
    /// This class is used for contact email to map the fields
    /// </summary>
    public class AdditionalEmailContactMapping : Profile
    {
        public AdditionalEmailContactMapping()
        {
            CreateMap<ContactEmailModel, ContactEmailCommand>();
        }
    }
}
