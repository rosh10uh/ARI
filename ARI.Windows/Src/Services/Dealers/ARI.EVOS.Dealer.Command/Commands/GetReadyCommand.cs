using Chassis.Command.Interfaces;
using System.Collections.Generic;

namespace ARI.EVOS.Dealer.Command.Commands
{
    /// <summary>
    /// This class contains add get ready command properties
    /// </summary>
    public class GetReadyCommand : ICommand<string>
    {
        public string CountryCode { get; private set; }
        public string DealerId { get; private set; }
        public string MakeCode { get; private set; }
        public List<string> GetReadyCategories { get; private set; }
        public string GetReadyCategory { get; private set; }

        public GetReadyCommand()
        {
        }
    }
}
