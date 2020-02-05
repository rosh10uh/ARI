using CSharpFunctionalExtensions;
using System.Threading.Tasks;
using Chassis.Command.Interfaces;
using Chassis.Dapper.Interfaces.Query;
using Chassis.Query.Interfaces;

namespace ARI.EVOS.Dealer.AppServices
{
    /// <summary>
    /// Base class to set basic thing for app service
    /// </summary>
    public abstract class BaseAppService
    {
        protected readonly ICommandBus CommandBus;
        protected readonly IQueryDispatcher QueryDispatcher;

        protected BaseAppService(ICommandBus commandBus, IQueryDispatcher queryDispatcher)
        {
            CommandBus = commandBus;
            QueryDispatcher = queryDispatcher;
        }

        /// <summary>
        /// Dispatch Query
        /// </summary>        
        protected Task<Maybe<T>> DispatchQuery<T>(IQuery<T> query)
        {
            return QueryDispatcher.DispatchAsync(query);
        }

        /// <summary>
        /// Dispatch Command
        /// </summary>        
        protected Task<Result<TResult>> DispatchCommand<TCommand, TResult>(object model)
            where TCommand : ICommand<TResult>
        {
            return CommandBus.DispatchAsync<TCommand, TResult>(model);
        }
    }
}
