using ARI.EVOS.Dealer.Command.Commands;
using ARI.EVOS.Dealer.Command.Interface;
using ARI.EVOS.Dealer.Domain.Models.Dealer.Entity;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
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
    /// This class is used to handle delete get ready command handler events.
    /// </summary>
    [Service]
    public class DeleteGetReadyCommandHandler : ICommandHandler<DeleteGetReadyCommand, string>
    {
        private readonly IDealerUnitOfWork _dealerUnitOfWork;
        private readonly IGetReadyValidation _getReadyValidation;

        public DeleteGetReadyCommandHandler(IDealerUnitOfWork dealerUnitOfWork, IGetReadyValidation getReadyValidation)
        {
            _dealerUnitOfWork = dealerUnitOfWork;
            _getReadyValidation = getReadyValidation;
        }
        /// <summary>
        ///  Call Repository method for Update Dealer's Detail
        /// </summary>
        /// <param name="commandDelete"></param>
        /// <returns>Task<Result<string>></returns>
        public async Task<Result<string>> HandleAsync(DeleteGetReadyCommand commandDelete)
        {
            foreach (var grVehicle in commandDelete.GetReadyCategories)
            {
                var countryCode = CountryCode.Create(commandDelete.CountryCode);
                var make = MakeCode.Create(commandDelete.MakeCode, countryCode);
                var dealerId = DealerId.Create(commandDelete.DealerId);

                var vehicleDet = GetVehicleDetail(countryCode, make, dealerId, grVehicle);

                if (vehicleDet != null && vehicleDet.Result.Value.Count() > 0)
                {
                    var vehicle = vehicleDet.Result.Value.ToList().FirstOrDefault();
                    await DeleteGetReady(vehicle);                   
                }
                else
                {
                    string errMsg = string.Format("{0} {1}", grVehicle, CommandConstant.ErrMessageGetReadyDeleted);
                    ErrorMessage(errMsg);
                }
            }
            return Result.Success<string>("Success");
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
        /// Delete get ready detail into db
        /// </summary>
        /// <param name="getReady"></param>
        /// <returns></returns>
        private async Task<Result<string>> DeleteGetReady(GetReady getReady)
        {
            try
            {
                // Delete Logic                            
                await _dealerUnitOfWork.BeginTransactionAsync();
                await _dealerUnitOfWork.GetReadyRepository.DeleteGetReadyDetail(getReady);
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
        /// Display Error messge on the screen
        /// </summary>
        /// <param name="errMessage"></param>
        private void ErrorMessage(string errMessage)
        {
            throw new ArgumentException(errMessage);
        }
    }
}
