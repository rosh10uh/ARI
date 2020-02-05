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
using System.Linq;
using System.Threading.Tasks;

namespace ARI.EVOS.Dealer.Command.CommandHandlers
{
    /// <summary>
    /// This class is used to handle update dealer command handler events.
    /// </summary>
    [Service]
    public class UpdateDealerCommandHandler : ICommandHandler<UpdateDealerCommand, string>
    {
        private readonly IDealerValidation _dealerValidation;
        private readonly IDealerUnitOfWork _dealerUnitOfWork;

        public UpdateDealerCommandHandler(IDealerUnitOfWork dealerUnitOfWork, IDealerValidation dealerValidation)
        {
            _dealerUnitOfWork = dealerUnitOfWork;
            _dealerValidation = dealerValidation;
        }

        /// <summary>
        ///  Call Repository method for Update Dealer
        /// </summary>
        /// <param name="updateDealerCommand"></param>
        public async Task<Result<string>> HandleAsync(UpdateDealerCommand command)
        {
            if (_dealerValidation.IsValid(command))
            {
                var countryCode = CountryCode.Create(command.CountryCode);
                var make = MakeCode.Create(command.MakeCode, countryCode);
                var dealerId = DealerId.Create(command.DealerId);
                var fetchData = await _dealerUnitOfWork.DealerRepository.GetByDealerIdAsync(countryCode, make, dealerId);
                var result = fetchData.Value.FirstOrDefault();

                if (fetchData.HasValue)
                {
                    if (fetchData.Value.Count > 0)
                    {
                        var dealer = GetDealerDomain(command, result, countryCode, make, dealerId);
                        return await UpdateDealer(dealer);
                    }
                    else
                    {
                        //validation message update/insert
                    }
                }
            }
            return Result.Failure<string>("Fail");
        }

        /// <summary>
        /// Set and Update dealer domain object
        /// </summary>
        /// <param name="updateDealerCommand"></param>
        /// <param name="dealer"></param>
        /// <returns></returns>
        private Domain.Models.Dealer.Aggregate.Dealer GetDealerDomain(UpdateDealerCommand updateDealerCommand, Domain.Models.Dealer.Aggregate.Dealer dealer, CountryCode countryCode, MakeCode make, DealerId dealerId)
        {
            // Vendor
            if (dealer.Vendor != null)
            {
                dealer.Vendor.Update(updateDealerCommand.VendorId, updateDealerCommand.VendorName);
            }
            else
            {
                var vendor = Vendor.Create(updateDealerCommand.VendorId, updateDealerCommand.VendorName);
                dealer.SetVendor(vendor);
            }

            // ContactInformation
            var phoneNumber1 = PhoneNumber.Create(updateDealerCommand.Phone1Number, countryCode);
            var phoneNumber2 = PhoneNumber.Create(updateDealerCommand.Phone2Number, countryCode);
            var fax = Fax.Create(updateDealerCommand.FaxNumber, countryCode);
            var email = Email.Create(updateDealerCommand.Email);
            dealer.ContactInformation.Update(updateDealerCommand.Contact1, updateDealerCommand.Contact2, phoneNumber1, phoneNumber2, updateDealerCommand.Phone1Exchange, updateDealerCommand.Phone2Exchange);
            dealer.ContactInformation.SetOtherContactInformation(fax, email);

            // Address
            if (dealer.Address != null)
            {
                dealer.Address.Update(updateDealerCommand.City, updateDealerCommand.ZipCode, updateDealerCommand.State, updateDealerCommand.Address1, updateDealerCommand.Address2, updateDealerCommand.Address3, updateDealerCommand.Address4);
            }
            else
            {
                var address = Address.Create(updateDealerCommand.City, updateDealerCommand.ZipCode, updateDealerCommand.State, updateDealerCommand.Address1, updateDealerCommand.Address2, updateDealerCommand.Address3, updateDealerCommand.Address4);
                dealer.SetAddress(address);
            }
            // User
            var creationUser = User.Create(updateDealerCommand.CreationUser);
            var lastUser = User.Create("PPage");

            // AdditionalInformation
            var bAcCode = BacCode.Create(updateDealerCommand.GmBusnAsctCd, make);
            var sellingDelivery = SellingDelivery.Create(updateDealerCommand.SellingDelivInd);
            var minatoryIndicator = MinoryIndicator.Create(updateDealerCommand.MinorityInd);
            var dealerRating = DealerRating.Create(updateDealerCommand.DealerRating);
            var paymentVia = PaymentVia.Create(updateDealerCommand.PymtVia);
            dealer.AdditionalInformation.Update(dealerRating, paymentVia, bAcCode, sellingDelivery, minatoryIndicator);
            dealer.AdditionalInformation.SetOtherInformation(updateDealerCommand.MfgZoneCode, updateDealerCommand.PrintDealerDraft, updateDealerCommand.DraftAcct1, updateDealerCommand.DraftAcct2, updateDealerCommand.TaxId, updateDealerCommand.TermsOverride);

            dealer.AdditionalInformation.SetDealerInfo(updateDealerCommand.PayToVendor, updateDealerCommand.FactoryCountPrior12, updateDealerCommand.StockCountPrior12, updateDealerCommand.CourtesyDelivPrintInd, updateDealerCommand.CertInd);

            // BankDetail
            var bankAccount = BankAccount.Create(updateDealerCommand.BankAccount);
            var bankCity = BankCity.Create(updateDealerCommand.BankCity, make, dealerId);
            if (dealer.BankDetail != null)
            {
                dealer.BankDetail.Update(bankAccount, bankCity, updateDealerCommand.BankNumber, updateDealerCommand.BankName);
            }
            else
            {
                var bankDetail = BankDetail.Create(updateDealerCommand.BankNumber, bankAccount, updateDealerCommand.BankName, bankCity);
                dealer.SetBankAccount(bankDetail);
            }

            // Dealer Domain
            dealer.SetDealerInfo(updateDealerCommand.DealerComments, Program.frmDealerNetwork, DateTime.UtcNow, DateTime.UtcNow,
                Program.frmDealerNetwork, DateTime.UtcNow);
            dealer.SetDealerOtherInfo(updateDealerCommand.KdClient, updateDealerCommand.KdDiv, updateDealerCommand.CanVolumeFactory,
                updateDealerCommand.CanVolumeStock, updateDealerCommand.UkCommission);
            dealer.SetUser(creationUser, lastUser);

            return dealer;
        }

        /// <summary>
        /// Update dealer into db
        /// </summary>
        /// <param name="dealer"></param>
        /// <returns></returns>
        private async Task<Result<string>> UpdateDealer(Domain.Models.Dealer.Aggregate.Dealer dealer)
        {
            try
            {
                // Update Logic                            
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
    }
}
