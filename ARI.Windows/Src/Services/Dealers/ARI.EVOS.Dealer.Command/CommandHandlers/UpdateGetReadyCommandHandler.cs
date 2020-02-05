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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARI.EVOS.Dealer.Command.CommandHandlers
{
    /// <summary>
    /// This class is used to handle update get ready command handler events.
    /// </summary>
    [Service]
    public class UpdateGetReadyCommandHandler : ICommandHandler<UpdateGetReadyCommand, string>
    {
        private readonly IDealerUnitOfWork _dealerUnitOfWork;
        private readonly IGetReadyValidation _getReadyValidation;

        public UpdateGetReadyCommandHandler(IDealerUnitOfWork dealerUnitOfWork, IGetReadyValidation getReadyValidation)
        {
            _dealerUnitOfWork = dealerUnitOfWork;
            _getReadyValidation = getReadyValidation;
        }

        /// <summary>
        ///  Call Repository method for Update Dealer's Detail
        /// </summary>
        /// <param name="commandUpdate"></param>
        /// <returns>Task<Result<string>></returns>
        public async Task<Result<string>> HandleAsync(UpdateGetReadyCommand command)
        {
            if (_getReadyValidation.IsValid(command))
            {
                if (command.GetReadyCategories.Count > 0)
                {
                    foreach (var grVehicle in command.GetReadyCategories)
                    {
                        var countryCode = CountryCode.Create(command.CountryCode);
                        var make = MakeCode.Create(command.MakeCode, countryCode);
                        var dealerId = DealerId.Create(command.DealerId);
                        var effectiveDate = EffectiveDate.Create(command.GetReadyEffectiveDate);
                        var lastUser = User.Create("PPage");
                        await SetUpdateVehicle(command, countryCode, make, dealerId, grVehicle, effectiveDate, lastUser);
                    }
                }
                else
                {
                    ErrorMessage(CommandConstant.ErrMessageRequiredGRVehicle);
                }
            }
            return Result.Success<string>("Success");
        }

        /// <summary>
        /// Set update vehicle
        /// </summary>
        private async Task SetUpdateVehicle(UpdateGetReadyCommand updateGetReadyCommand, CountryCode countryCode, MakeCode make,
            DealerId dealerId, string grVehicle, EffectiveDate effectiveDate, User lastUser)
        {
            var vehicleDet = GetVehicleDetail(countryCode, make, dealerId, grVehicle);
            if (vehicleDet != null && vehicleDet.Result.Value.Any())
            {
                var fetchData = await _dealerUnitOfWork.GetReadyRepository.GetByGetReadyVehicleAsync(countryCode, make, dealerId, grVehicle);
                var result = fetchData.Value.FirstOrDefault();
                result.Update(countryCode, make, dealerId);
                result.SetGetReadyInfo(grVehicle, updateGetReadyCommand.ClientId, updateGetReadyCommand.GetReadyAmount, effectiveDate,
                    Program.frmDealerNetwork, lastUser, DateTime.UtcNow);
                await UpdateGetReady(result);
            }
            else
            {
                string errMsg = string.Format("{0} {1}", grVehicle, CommandConstant.ErrMessageGRVehicleNotExist);
                ErrorMessage(errMsg);
            }
        }

        /// <summary>
        ///  Get Vehicle detail of delear
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="makeCode"></param>
        /// <param name="dealerId"></param>
        /// <param name="grVehicle"></param>
        /// <returns>Task<CSharpFunctionalExtensions.Maybe<List<GetReady>>> </returns>
        private Task<Maybe<List<GetReady>>> GetVehicleDetail(CountryCode countryCode, MakeCode makeCode, DealerId dealerId, string grVehicle)
        {
            return _dealerUnitOfWork.GetReadyRepository.GetByGetReadyVehicleAsync(countryCode, makeCode, dealerId, grVehicle);
        }

        /// <summary>
        /// Update get ready detail into db
        /// </summary>
        /// <param name="getReady"></param>
        /// <returns></returns>
        private async Task<Result<string>> UpdateGetReady(GetReady getReady)
        {
            try
            {
                // Update Logic                            
                await _dealerUnitOfWork.BeginTransactionAsync();
                await _dealerUnitOfWork.GetReadyRepository.AddOrUpdateGetReady(getReady);
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
        /// Display Error messge on the screen
        /// </summary>
        /// <param name="errMessage"></param>
        private void ErrorMessage(string errMessage)
        {
            throw new ArgumentException(errMessage);
        }
    }
}
