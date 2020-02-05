using Chassis.Command.Interfaces;

namespace ARI.EVOS.Dealer.Command.Commands
{
    /// <summary>
    /// This class contains delete dealer command properties
    /// </summary>
    public class DeleteDealerCommand : ICommand<string>
    {
        public string CountryCode { get; private set; }
        public string MakeCode { get; private set; }
        public string DealerId { get; private set; }
    }
}
