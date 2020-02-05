using ARI.EVOS.Dealer.Command.Commands;
using ARI.EVOS.Dealer.Command.Interface;
using ARI.EVOS.Dealer.Domain.SharedKernel;
using ARI.EVOS.Dealer.Infrastructure.Interface.Repositories;
using CSharpFunctionalExtensions;
using System;
using System.Threading.Tasks;
using System.Linq;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Chassis.Command.Interfaces;
using Chassis.RegisterServices.Attributes;
using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;

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
        /// <param name="commandUpdate"></param>
        public async Task<Result<string>> HandleAsync(UpdateDealerCommand commandUpdate)
        {
            if (_dealerValidation.IsValid(commandUpdate))
            {
                var countryCode = CountryCode.Create(commandUpdate.CountryCode);
                var make = MakeCode.Create(commandUpdate.MakeCode, countryCode);
                var dealerId = DealerId.Create(commandUpdate.DealerId);
                var fetchData = await _dealerUnitOfWork.DealerRepository.GetByDealerIdAsync(countryCode, make, dealerId);
                var result = fetchData.Value.ToList().FirstOrDefault();

                if (fetchData.HasValue)
                {
                    if (fetchData.Value.Count > 0)
                    {
                        var dealer = GetDealerDomain(commandUpdate, result, countryCode, make, dealerId);
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
        /// <param name="commandUpdate"></param>
        /// <param name="dealer"></param>
        /// <returns></returns>
        private Domain.Models.Dealer.Aggregate.Dealer GetDealerDomain(UpdateDealerCommand commandUpdate, Domain.Models.Dealer.Aggregate.Dealer dealer, CountryCode countryCode, MakeCode make, DealerId dealerId)
        {
            // Vendor
            if (dealer.Vendor != null)
            {
                dealer.Vendor.Update(commandUpdate.VendorId, commandUpdate.VendorName);
            }
            else
            {
                var vendor = Vendor.Create(commandUpdate.VendorId, commandUpdate.VendorName);
                dealer.SetVendor(vendor);
            }

            // ContactInformation
            var phoneNumber1 = PhoneNumber.Create(commandUpdate.Phone1Number, countryCode);
            var phoneNumber2 = PhoneNumber.Create(commandUpdate.Phone2Number, countryCode);
            var fax = Fax.Create(commandUpdate.FaxNumber, countryCode);
            var email = Email.Create(commandUpdate.Email);
            dealer.ContactInformation.Update(commandUpdate.Contact1, commandUpdate.Contact2, phoneNumber1, phoneNumber2, commandUpdate.Phone1Exchange, commandUpdate.Phone2Exchange, fax, email);

            // Address
            if(dealer.Address != null)
            { 
                dealer.Address.Update(commandUpdate.City, commandUpdate.ZipCode, commandUpdate.State, commandUpdate.Address1, commandUpdate.Address2, commandUpdate.Address3, commandUpdate.Address4);
            }
            else
            {
                var address = Address.Create(commandUpdate.City, commandUpdate.ZipCode, commandUpdate.State, commandUpdate.Address1, commandUpdate.Address2, commandUpdate.Address3, commandUpdate.Address4);
                dealer.SetAddress(address);
            }
            // User
            var creationUser = User.Create(commandUpdate.CreationUser);
            var lastUser = User.Create(commandUpdate.LastUser);

            // AdditionalInformation
            var bAcCode = BacCode.Create(commandUpdate.GmBusnAsctCd, make);
            var sellingDelivery = SellingDelivery.Create(commandUpdate.SellingDelivInd);
            var minatoryIndicator = MinoryIndicator.Create(commandUpdate.MinorityInd);
            var dealerRating = DealerRating.Create(commandUpdate.DealerRating);
            var paymentVia = PaymentVia.Create(commandUpdate.PymtVia);
            dealer.AdditionalInformation.Update(dealerRating, paymentVia, bAcCode, sellingDelivery, minatoryIndicator);
            dealer.AdditionalInformation.UpdateOtherInformation(commandUpdate.MfgZoneCode, commandUpdate.PrintDealerDraft, commandUpdate.DraftAcct1, commandUpdate.DraftAcct2, commandUpdate.TaxId, commandUpdate.TermsOverride, commandUpdate.PayToVendor, commandUpdate.FactoryCountPrior12, commandUpdate.StockCountPrior12, commandUpdate.CourtesyDelivPrintInd, commandUpdate.CertInd);

            // BankDetail
            var bankAccount = BankAccount.Create(commandUpdate.BankAccount);
            var bankCity = BankCity.Create(commandUpdate.BankCity, make, dealerId);
            if (dealer.BankDetail != null)
            {
                dealer.BankDetail.Update(bankAccount, bankCity, commandUpdate.BankNumber, commandUpdate.BankName);
            }
            else
            {
                var bankDetail = BankDetail.Create(commandUpdate.BankNumber, bankAccount, commandUpdate.BankName, bankCity);
                dealer.SetBankAccount(bankDetail);
            }

            // Dealer Domain
            dealer.SetDealerInfo(commandUpdate.DealerComments, Program.frmDealerNetwork, DateTime.UtcNow, DateTime.UtcNow, 
                Program.frmDealerNetwork, DateTime.UtcNow, commandUpdate.KdClient, commandUpdate.KdDiv, commandUpdate.CanVolumeFactory, 
                commandUpdate.CanVolumeStock, commandUpdate.UkCommission);
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
            catch (Exception ex)
            {
                await _dealerUnitOfWork.RollBackAsync();
                throw;
            }
        }
    }
}
