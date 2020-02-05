using ARI.EVOS.Dealer.Command.Commands;
using ARI.EVOS.Dealer.Command.Interface;
using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using ARI.EVOS.Dealer.Domain.SharedKernel;
using ARI.EVOS.Dealer.Infrastructure.Interface.Repositories;
using Chassis.Command.Interfaces;
using Chassis.RegisterServices.Attributes;
using CSharpFunctionalExtensions;
using System;
using System.Threading.Tasks;

namespace ARI.EVOS.Dealer.Command.CommandHandlers
{
    /// <summary>
    /// This class is used to handle insert dealer command handler events.
    /// </summary>
    [Service]
    public class AddDealerCommandHandler : ICommandHandler<AddDealerCommand, string>
    {
        private readonly IDealerValidation _dealerValidation;
        private readonly IDealerUnitOfWork _dealerUnitOfWork;

        public AddDealerCommandHandler(IDealerUnitOfWork dealerUnitOfWork, IDealerValidation dealerValidation)
        {
            _dealerUnitOfWork = dealerUnitOfWork;
            _dealerValidation = dealerValidation;
        }

        /// <summary>
        ///  Call Repository method for Insert Dealer
        /// </summary>
        /// <param name="command"></param>
        public async Task<Result<string>> HandleAsync(AddDealerCommand command)
        {
            if (_dealerValidation.IsValid(command))
            {
                var countryCode = CountryCode.Create(command.CountryCode);
                var make = MakeCode.Create(command.MakeCode, countryCode);
                var dealerId = DealerId.Create(command.DealerId);
                var dealer = GetDealerDomain(command, countryCode, make, dealerId);

                var fetchData = await _dealerUnitOfWork.DealerRepository.GetByDealerIdAsync(countryCode, make, dealerId);
                if (fetchData.HasValue)
                {
                    if (fetchData.Value.Count > 0)
                    {
                        ErrorMessage(CommandConstant.DealerExistValidation);
                    }
                    else
                    {
                        return await InsertDealer(dealer);
                    }
                }
            }
            return Result.Failure<string>("Fail");
        }

        /// <summary>
        /// Factory for throw exception
        /// </summary>        
        private static void ErrorMessage(string errMessage)
        {
            throw new ArgumentException(errMessage);
        }

        /// <summary>
        /// Add dealer into db
        /// </summary>
        /// <param name="dealer"></param>
        /// <returns></returns>
        private async Task<Result<string>> InsertDealer(Domain.Models.Dealer.Aggregate.Dealer dealer)
        {
            try
            {
                // Insert Logic                            
                await _dealerUnitOfWork.BeginTransactionAsync();
                await _dealerUnitOfWork.DealerRepository.AddOrUpdateDealer(dealer);
                await _dealerUnitOfWork.CommitAsync();
                return Result.Success<string>("Success");
            }
            catch (Exception)
            {
                await _dealerUnitOfWork.RollBackAsync();
                throw;
            }
        }

        /// <summary>
        /// Create dealer domain object
        /// </summary>
        /// <param name="addDealerCommand"></param>
        /// <param name="countryCode"></param>
        /// <param name="make"></param>
        /// <param name="dealerId"></param>
        /// <returns></returns>
        private Domain.Models.Dealer.Aggregate.Dealer GetDealerDomain(AddDealerCommand addDealerCommand, CountryCode countryCode, MakeCode make, DealerId dealerId)
        {
            // Vendor
            var vendor = Vendor.Create(addDealerCommand.VendorId, addDealerCommand.VendorName);

            // ContactInformation
            var phoneNumber1 = PhoneNumber.Create(addDealerCommand.Phone1Number, countryCode);
            var phoneNumber2 = PhoneNumber.Create(addDealerCommand.Phone2Number, countryCode);
            var fax = Fax.Create(addDealerCommand.FaxNumber, countryCode);
            var email = Email.Create(addDealerCommand.Email);
            var contactInformation = ContactInformation.Create(addDealerCommand.Contact1, addDealerCommand.Contact2, phoneNumber1, phoneNumber2, addDealerCommand.Phone1Exchange, addDealerCommand.Phone2Exchange);
            contactInformation.SetOtherContactInformation(fax, email);

            // Address
            var address = Address.Create(addDealerCommand.City, addDealerCommand.ZipCode, addDealerCommand.State, addDealerCommand.Address1, addDealerCommand.Address2, addDealerCommand.Address3, addDealerCommand.Address4);

            // User
            var creationUser = User.Create(addDealerCommand.CreationUser);
            var lastUser = User.Create("PPage");

            // AdditionalInformation
            var bAcCode = BacCode.Create(addDealerCommand.GmBusnAsctCd, make);
            var sellingDelivery = SellingDelivery.Create(addDealerCommand.SellingDelivInd);
            var minatoryIndicator = MinoryIndicator.Create(addDealerCommand.MinorityInd);
            var dealerRating = DealerRating.Create(addDealerCommand.DealerRating);
            var paymentVia = PaymentVia.Create(addDealerCommand.PymtVia);
            var dealerOtherInformation = DealerOtherInformation.Create(dealerRating, paymentVia);
            dealerOtherInformation.SetInformation(bAcCode, sellingDelivery, minatoryIndicator);
            dealerOtherInformation.SetOtherInformation(addDealerCommand.MfgZoneCode, addDealerCommand.PrintDealerDraft, addDealerCommand.DraftAcct1, addDealerCommand.DraftAcct2, addDealerCommand.TaxId, addDealerCommand.TermsOverride);
            dealerOtherInformation.SetDealerInfo(addDealerCommand.PayToVendor, addDealerCommand.FactoryCountPrior12, addDealerCommand.StockCountPrior12, addDealerCommand.CourtesyDelivPrintInd, addDealerCommand.CertInd);

            // BankDetail
            var bankAccount = BankAccount.Create(addDealerCommand.BankAccount);
            var bankCity = BankCity.Create(addDealerCommand.BankCity, make, dealerId);
            var bankDetail = BankDetail.Create(addDealerCommand.BankNumber, bankAccount, addDealerCommand.BankName, bankCity);

            // Dealer Domain
            var dealer = Domain.Models.Dealer.Aggregate.Dealer.Create(countryCode, make, dealerId);
            dealer.SetVendor(vendor);
            dealer.SetContactInformation(contactInformation);
            dealer.SetAddress(address);
            dealer.SetDealerInfo(addDealerCommand.DealerComments, Program.frmDealerNetwork, DateTime.UtcNow, DateTime.UtcNow,
                Program.frmDealerNetwork, DateTime.UtcNow);
            dealer.SetDealerOtherInfo(addDealerCommand.KdClient, addDealerCommand.KdDiv, addDealerCommand.CanVolumeFactory,
                addDealerCommand.CanVolumeStock, addDealerCommand.UkCommission);
            dealer.SetUser(creationUser, lastUser);
            dealer.SetAdditionalInformation(dealerOtherInformation);
            dealer.SetBankAccount(bankDetail);

            return dealer;
        }
    }
}
