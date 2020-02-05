using ARI.EVOS.Dealer.Command.Commands;

namespace ARI.EVOS.Dealer.Command.Interface
{
    /// <summary>
    /// This interface contains validation for dealer
    /// </summary>
    public interface IDealerValidation
    {
        bool IsValid(DealerCommand dealerCommand);
    }
}
