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
    /// This class is used to handle insert get ready command handler events.
    /// </summary>
    [Service]
    public class AddGetReadyCommandHandler : ICommandHandler<AddGetReadyCommand, string>
    {
        private readonly IGetReadyValidation _getReadyValidation;
        private readonly IDealerUnitOfWork _dealerUnitOfWork;
        public AddGetReadyCommandHandler(IDealerUnitOfWork dealerUnitOfWork, IGetReadyValidation getReadyValidation)
        {
            _dealerUnitOfWork = dealerUnitOfWork;
            _getReadyValidation = getReadyValidation;
        }
        /// <summary>
        ///  Call Repository method for Insert get ready detail
        /// </summary>
        /// <param name="commandAdd"></param>
        public async Task<Result<string>> HandleAsync(AddGetReadyCommand commandAdd)
        {
            if (_getReadyValidation.IsValid(commandAdd))
            {
                var countryCode =CountryCode.Create(commandAdd.CountryCode);
                var make = MakeCode.Create(commandAdd.MakeCode, countryCode);
                var dealerId = DealerId.Create(commandAdd.DealerId);

                var isExist = await _dealerUnitOfWork.DealerRepository.GetByDealerIdAsync(countryCode, make, dealerId);
                if (isExist.HasValue)
                {
                    if (isExist.Value.Count > 0)
                    {
                        return await ValidateGetReady(commandAdd, countryCode, make, dealerId);
                    }
                    else
                    {
                        ErrorMessage(CommandConstant.ErrMessageDealerNotExist);
                    }
                }
            }
            return Result.Failure<string>("Fail");
        }

        /// <summary>
        /// Validate Get Ready Details before insert
        /// </summary>
        /// <param name="commandAdd"></param>
        /// <param name="countryCode"></param>
        /// <param name="make"></param>
        /// <param name="dealerId"></param>
        /// <returns></returns>
        private async Task<Result<string>> ValidateGetReady(AddGetReadyCommand commandAdd, CountryCode countryCode, MakeCode make, DealerId dealerId)
        {
            // Get last value of get ready id 
            if (commandAdd.GetReadyCategories.Count > 0)
            {
                foreach (var vehicle in commandAdd.GetReadyCategories)
                {
                    countryCode = CountryCode.Create(commandAdd.CountryCode);
                    make = MakeCode.Create(commandAdd.MakeCode, countryCode);
                    dealerId = DealerId.Create(commandAdd.DealerId);
                    var effectiveDate = EffectiveDate.Create(commandAdd.GetReadyEffectiveDate);
                    var lastUser = User.Create(commandAdd.LastUser);
                    if (IsNotExistGrVehicle(countryCode, make, dealerId, vehicle))
                    {
                        var getReady = GetReady.Create(countryCode, make, dealerId);
                        getReady.SetGetReadyInfo(vehicle, commandAdd.ClientId, commandAdd.GetReadyAmount, effectiveDate, Program.frmDealerNetwork, lastUser, DateTime.UtcNow);
                        await InsertGetReady(getReady);
                    }
                }
            }
            else
            {
                ErrorMessage(CommandConstant.ErrMessageRequiredGRVehicle);
            }
            return Result.Success<string>("Success");
        }

        /// <summary>
        /// Add get ready detail into db
        /// </summary>
        /// <param name="getReady"></param>
        /// <returns></returns>
        private async Task<Result<string>> InsertGetReady(GetReady getReady)
        {
            try
            {
                // Insert Logic                            
                await _dealerUnitOfWork.BeginTransactionAsync();
                await _dealerUnitOfWork.GetReadyRepository.AddOrUpdateGetReady(getReady);
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
        /// Check Vehicle Exist for Dealer 
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="makeCode"></param>
        /// <param name="dealerId"></param>
        /// <param name="grVehicle"></param>
        /// <returns>bool</returns>
        private bool IsNotExistGrVehicle(CountryCode countryCode, MakeCode makeCode, DealerId dealerId, string grVehicle)
        {
            var result = GetVehicleDetail(countryCode, makeCode, dealerId, grVehicle);

            if (result.Result.Value.Count() > 0)
            {
                string errMsg = string.Format("{0} {1}", grVehicle, CommandConstant.ErrMessageGRVehicleExist);
                ErrorMessage(errMsg);
                return false;
            }
            return true;
        }

        /// <summary>
        ///  Get Vehicle detail of dealer
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="makeCode"></param>
        /// <param name="dealerId"></param>
        /// <param name="grVehical"></param>
        /// <returns>Task<Maybe<List<GetReady>>> </returns>
        private Task<Maybe<List<GetReady>>> GetVehicleDetail(CountryCode countryCode, MakeCode makeCode, DealerId dealerId, string grVehical)
        {
            return _dealerUnitOfWork.GetReadyRepository.GetByGetReadyVehicleAsync(countryCode, makeCode, dealerId, grVehical);
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
