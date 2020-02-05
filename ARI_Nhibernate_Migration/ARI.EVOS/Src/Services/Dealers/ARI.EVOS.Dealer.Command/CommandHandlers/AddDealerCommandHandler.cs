using ARI.EVOS.Dealer.Command.Commands;
using ARI.EVOS.Dealer.Command.Interface;
using ARI.EVOS.Dealer.Domain.SharedKernel;
using ARI.EVOS.Dealer.Infrastructure.Interface.Repositories;
using CSharpFunctionalExtensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Chassis.Command.Interfaces;
using Chassis.RegisterServices.Attributes;

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
        /// <param name="commandAdd"></param>
        public async Task<Result<string>> HandleAsync(AddDealerCommand commandAdd)
        {
            if (_dealerValidation.IsValid(commandAdd))
            {
                var countryCode = CountryCode.Create(commandAdd.CountryCode);
                var make = MakeCode.Create(commandAdd.MakeCode, countryCode);
                var dealerId = DealerId.Create(commandAdd.DealerId);
                var dealer = GetDealerDomain(commandAdd, countryCode, make, dealerId);

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
            catch (Exception ex)
            {
                await _dealerUnitOfWork.RollBackAsync();
                throw;
            }
        }

        /// <summary>
        /// Create dealer domain object
        /// </summary>
        /// <param name="commandAdd"></param>
        /// <param name="countryCode"></param>
        /// <param name="make"></param>
        /// <param name="dealerId"></param>
        /// <returns></returns>
        private Domain.Models.Dealer.Aggregate.Dealer GetDealerDomain(AddDealerCommand commandAdd, CountryCode countryCode, MakeCode make, DealerId dealerId)
        {
            // Vendor
            var vendor = Vendor.Create(commandAdd.VendorId, commandAdd.VendorName);

            // ContactInformation
            var phoneNumber1 = PhoneNumber.Create(commandAdd.Phone1Number, countryCode);
            var phoneNumber2 = PhoneNumber.Create(commandAdd.Phone2Number, countryCode);
            var fax = Fax.Create(commandAdd.FaxNumber, countryCode);
            var email = Email.Create(commandAdd.Email);
            var contactInformation = ContactInformation.Create(commandAdd.Contact1, commandAdd.Contact2, phoneNumber1, phoneNumber2, commandAdd.Phone1Exchange, commandAdd.Phone2Exchange, fax, email);

            // Address
            var address = Address.Create(commandAdd.City, commandAdd.ZipCode, commandAdd.State, commandAdd.Address1, commandAdd.Address2, commandAdd.Address3, commandAdd.Address4);

            // User
            var creationUser = User.Create(commandAdd.CreationUser);
            var lastUser = User.Create(commandAdd.LastUser);

            // AdditionalInformation
            var bAcCode = BacCode.Create(commandAdd.GmBusnAsctCd, make);
            var sellingDelivery = SellingDelivery.Create(commandAdd.SellingDelivInd);
            var minatoryIndicator = MinoryIndicator.Create(commandAdd.MinorityInd);
            var dealerRating = DealerRating.Create(commandAdd.DealerRating);
            var paymentVia = PaymentVia.Create(commandAdd.PymtVia);
            var dealerOtherInformation = DealerOtherInformation.Create(dealerRating, paymentVia);
            dealerOtherInformation.SetInformation(bAcCode, sellingDelivery, minatoryIndicator);
            dealerOtherInformation.SetOtherInformation(commandAdd.MfgZoneCode, commandAdd.PrintDealerDraft, commandAdd.DraftAcct1, commandAdd.DraftAcct2, commandAdd.TaxId, commandAdd.TermsOverride, commandAdd.PayToVendor, commandAdd.FactoryCountPrior12, commandAdd.StockCountPrior12, commandAdd.CourtesyDelivPrintInd, commandAdd.CertInd);

            // BankDetail
            var bankAccount = BankAccount.Create(commandAdd.BankAccount);
            var bankCity = BankCity.Create(commandAdd.BankCity, make, dealerId);
            var bankDetail = BankDetail.Create(commandAdd.BankNumber, bankAccount, commandAdd.BankName, bankCity);

            // Dealer Domain
            var dealer = Domain.Models.Dealer.Aggregate.Dealer.Create(countryCode, make, dealerId);
            dealer.SetVendor(vendor);
            dealer.SetContactInformation(contactInformation);
            dealer.SetAddress(address);
            dealer.SetDealerInfo(commandAdd.DealerComments, Program.frmDealerNetwork, DateTime.UtcNow, DateTime.UtcNow,
                Program.frmDealerNetwork, DateTime.UtcNow, commandAdd.KdClient, commandAdd.KdDiv, commandAdd.CanVolumeFactory,
                commandAdd.CanVolumeStock, commandAdd.UkCommission);
            dealer.SetUser(creationUser, lastUser);
            dealer.SetAdditionalInformation(dealerOtherInformation);
            dealer.SetBankAccount(bankDetail);

            return dealer;
        }
    }
}
