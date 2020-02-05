using System;

namespace ARI.EVOS.Dealer.Command.Commands
{
    /// <summary>
    /// This class contains update get ready command properties
    /// </summary>
    public class UpdateGetReadyCommand : GetReadyCommand
    {
        public string ClientId { get; private set; }
        public decimal? GetReadyAmount { get; private set; }
        public DateTime? GetReadyEffectiveDate { get; private set; }
        public string LastProgram { get; private set; }
        public string LastUser { get; private set; }
        public DateTime? LastChange { get; private set; }
        public UpdateGetReadyCommand()
        {

        }
    }
}
