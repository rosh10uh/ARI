using ARI.EVOS.Dealer.Command.Commands;

namespace ARI.EVOS.Dealer.Command.Interface
{
    /// <summary>
    /// This interface contains validation for contact email
    /// </summary>
    public interface IContactEmailValidation
    {
        bool IsValid(ContactEmailCommand commandAdd);
    }
}
