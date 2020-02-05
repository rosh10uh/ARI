﻿using ARI.EVOS.Dealer.Command.Commands;
using ARI.EVOS.Dealer.Infrastructure.Interface.Repositories;
using CSharpFunctionalExtensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Chassis.Command.Interfaces;
using Chassis.RegisterServices.Attributes;

namespace ARI.EVOS.Dealer.Command.CommandHandlers
{
    /// <summary>
    /// This class is used to handle delete dealer command handler events
    /// </summary>
    [Service]
    public class DeleteDealerCommandHandler : ICommandHandler<DeleteDealerCommand, string>
    {
        private readonly IDealerUnitOfWork _dealerUnitOfWork;

        public DeleteDealerCommandHandler(IDealerUnitOfWork dealerUnitOfWork)
        {
            _dealerUnitOfWork = dealerUnitOfWork;
        }

        /// <summary>
        /// This method is used to handle delete dealer
        /// </summary>
        /// <param name="commandDel"></param>
        /// <returns></returns>
        public async Task<Result<string>> HandleAsync(DeleteDealerCommand commandDel)
        {
            var countryCode = CountryCode.Create(commandDel.CountryCode);
            var make = MakeCode.Create(commandDel.MakeCode, countryCode);
            var dealerId = DealerId.Create(commandDel.DealerId);

            var fetchData = await _dealerUnitOfWork.DealerRepository.GetByDealerIdAsync(countryCode, make, dealerId);
            if (fetchData.HasValue && fetchData.Value.Count > 0)
            {
                var resultDealer = fetchData.Value.ToList().FirstOrDefault();

                if (resultDealer != null) return await DeleteDealer(resultDealer);
            }
            return Result.Failure<string>("Fail");
        }

        /// <summary>
        /// Delete dealer from database based on composite key
        /// </summary>
        /// <param name="dealer"></param>
        /// <returns></returns>
        private async Task<Result<string>> DeleteDealer(Domain.Models.Dealer.Aggregate.Dealer dealer)
        {
            try
            {
                await _dealerUnitOfWork.BeginTransactionAsync();
                await _dealerUnitOfWork.DealerRepository.DeleteDealer(dealer);
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
