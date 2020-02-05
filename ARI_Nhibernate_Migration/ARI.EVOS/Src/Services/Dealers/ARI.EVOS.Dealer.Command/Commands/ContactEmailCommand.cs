using ARI.EVOS.Dealers.Models;
using Chassis.Command.Interfaces;

namespace ARI.EVOS.Dealer.Command.Commands
{
    /// <summary>
    /// This class contains contact email command properties.
    /// </summary>
    public class ContactEmailCommand: ICommand<string>
    {
        public string DealerId { get;private set; }
        public string ContactType { get; private set; }
        public string StockContactName { get; private set; }
        public string StockContactEmail { get; private set; }
        public string FinanceContactName { get; private set; }
        public string FinanceContactEmail { get; private set; }
        public string LicenseContactName { get; private set; }
        public string LicenseContactEmail { get; private set; }
        public string MakeCode { get; private set; }
        public string CountryCode { get; private set; }

        public ContactEmailCommand()
        {
        }

    }
}
