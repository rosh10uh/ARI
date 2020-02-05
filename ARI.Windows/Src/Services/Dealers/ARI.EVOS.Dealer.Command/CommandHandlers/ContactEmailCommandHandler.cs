using ARI.EVOS.Dealer.Command.Commands;
using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using ARI.EVOS.Dealer.Domain.SharedKernel;
using ARI.EVOS.Dealer.Infrastructure.Interface.Repositories;
using ARI.EVOS.Infra;
using ARI.EVOS.Infra.Interface;
using Chassis.Command.Interfaces;
using Chassis.RegisterServices.Attributes;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ARI.EVOS.Dealer.Command.CommandHandlers
{
    /// <summary>
    /// This class is used to handle contact email command handler events.
    /// </summary>
    [Service]
    public class ContactEmailCommandHandler : ICommandHandler<ContactEmailCommand, string>
    {
        private readonly IContactEmailUnitOfWork _additionalEmailUnitOfWork;
        private int _recordCount = 0;
        private int _checkInsert = 0;
        protected readonly IMessage _message;

        public ContactEmailCommandHandler(IContactEmailUnitOfWork additionalEmailUnitOfWork, IMessage message)
        {
            _additionalEmailUnitOfWork = additionalEmailUnitOfWork;
            _message = message;
        }

        /// <summary>
        /// This method is used to handle add or update email 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<Result<string>> HandleAsync(ContactEmailCommand command)
        {
            var emailStock = SetContactEmail(command, ContactType.Stock);
            var emailFinance = SetContactEmail(command, ContactType.Finance);
            var emailLicense = SetContactEmail(command, ContactType.Licence);
            try
            {
                await _additionalEmailUnitOfWork.BeginTransactionAsync();
                await AddAdditionalMail(emailStock);
                await AddAdditionalMail(emailFinance);
                await AddAdditionalMail(emailLicense);
                await _additionalEmailUnitOfWork.CommitAsync();
                DisplayMessage();
                return Result.Success<string>("Success");
            }
            catch (Exception)
            {
                await _additionalEmailUnitOfWork.RollBackAsync();
                throw;
            }
        }

        /// <summary>
        /// Create contact email object
        /// </summary>
        /// <param name="command"></param>
        /// <param name="id"></param>
        /// <param name="contactType"></param>
        /// <returns></returns>
        private ContactEmail SetContactEmail(ContactEmailCommand command, ContactType contactType)
        {
            var countryCode = CountryCode.Create(command.CountryCode);
            var make = MakeCode.Create(command.MakeCode, countryCode);
            var dealerId = DealerId.Create(command.DealerId);

            ContactEmail contactEmail = null;
            Email email = null;
            string contactName = null;

            var emailData = GetContactEmailsList(countryCode, make, dealerId, contactType).GetAwaiter().GetResult();
            var contacts = emailData.Value.ToList();
            if (emailData.HasValue)
            {
                if (emailData.Value.Count > 0)
                {
                    _recordCount++;
                    contactEmail = contacts.FirstOrDefault();
                    SetContactInfo(command, contactType, ref email, ref contactName);
                    contactEmail?.SaveUpdateEmail(contactEmail, contactName, email);
                }
                else
                {
                    SetContactInfo(command, contactType, ref email, ref contactName);
                    contactEmail = ContactEmail.Create(countryCode, make, dealerId, contactType, contactName, email);
                    contactEmail.SaveAdditionalEmail(contactEmail);
                }
            }
            return contactEmail;

        }

        /// <summary>
        /// Set contact information
        /// </summary>
        /// <param name="command"></param>
        /// <param name="contactType"></param>
        /// <param name="email"></param>
        /// <param name="contactName"></param>
        private static void SetContactInfo(ContactEmailCommand command, ContactType contactType, ref Email email, ref string contactName)
        {
            if (contactType == ContactType.Stock)
            {
                email = Email.Create(command.StockContactEmail);
                contactName = command.StockContactName;
            }
            else if (contactType == ContactType.Finance)
            {
                email = Email.Create(command.FinanceContactEmail);
                contactName = command.FinanceContactName;
            }
            else if (contactType == ContactType.Licence)
            {
                email = Email.Create(command.LicenseContactEmail);
                contactName = command.LicenseContactName;
            }
        }

        /// <summary>
        /// Get contact email list
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="make"></param>
        /// <param name="dealerId"></param>
        /// <param name="contactType"></param>
        /// <returns></returns>
        private async Task<Maybe<List<ContactEmail>>> GetContactEmailsList(CountryCode countryCode, MakeCode make, DealerId dealerId, ContactType contactType)
        {
            var fetchData = await _additionalEmailUnitOfWork.ContactEmailRepository.CheckContactEmail(countryCode, make, dealerId, contactType);
            return fetchData;
        }

        /// <summary>
        /// Add additional email
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        private async Task AddAdditionalMail(ContactEmail contact)
        {
            if (contact != null && !string.IsNullOrEmpty(contact.Name) && !string.IsNullOrEmpty(contact.Email.EmailAddress))
            {
                _checkInsert++;
                await _additionalEmailUnitOfWork.ContactEmailRepository.AddOrUpdateEmail(contact);
            }
        }

        /// <summary>
        /// Display message when record inserted or updated
        /// </summary>
        private void DisplayMessage()
        {
            if (_checkInsert > 0)
            {
                if (_recordCount > 0)
                {
                    _message.Show(CommandConstant.UpdatedEmail, BaseConstant.InformationMessageBoxTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _message.Show(CommandConstant.InsertedEmail, BaseConstant.InformationMessageBoxTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                if (_recordCount == 0)
                {
                    _message.Show(CommandConstant.ErrMessageEmail, BaseConstant.ValidationMessageBoxTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}
