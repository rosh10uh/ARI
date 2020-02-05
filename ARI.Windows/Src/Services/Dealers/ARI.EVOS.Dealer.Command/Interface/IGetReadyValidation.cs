using ARI.EVOS.Dealer.Command.Commands;

namespace ARI.EVOS.Dealer.Command.Interface
{
    /// <summary>
    /// This interface contains validation for get ready
    /// </summary>
    public interface IGetReadyValidation
    {
        bool IsValid(AddGetReadyCommand addGetReadyCommand);
        bool IsValid(UpdateGetReadyCommand updateGetReadyCommand);

    }
}
