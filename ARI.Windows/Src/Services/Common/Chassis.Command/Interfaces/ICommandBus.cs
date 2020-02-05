using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Chassis.Command.Interfaces
{
    /// <summary>
    /// Implement this interface in command bus 
    /// </summary>
    public interface ICommandBus
    {
        /// <summary>
        /// Get result based on command handler
        /// </summary>
        /// <typeparam name="TCommand">Pass command type class which inherits ICommand</typeparam>
        /// <typeparam name="TResult">Pass return type of handle method</typeparam>
        /// <param name="command">Pass command to call the handle method of command handler</param>
        /// <returns>It returns the result of handle method</returns>
        Task<Result<TResult>> DispatchAsync<TResult>(ICommand<TResult> command);

        /// <summary>
        /// Get result based on command handler
        /// </summary>
        /// <typeparam name="TCommand">Used to pass command type class which inherits ICommand</typeparam>
        /// <typeparam name="TResult">Pass return type of handle method</typeparam>
        /// <param name="object">Pass model to map with command and command will pass to the handler</param>
        /// <returns>It returns the result of handle method</returns>
        Task<Result<TResult>> DispatchAsync<TCommand, TResult>(object model) where TCommand : ICommand<TResult>;

        /// <summary>
        /// Get result based on command handler
        /// </summary>
        /// <typeparam name="TCommand">Pass command type class which inherits ICommand</typeparam>
        /// <typeparam name="TMetadata">Pass meta-data as a type, meta-data should be any type of class,parameters</typeparam>
        /// <typeparam name="TResult">Pass return type of handle method</typeparam>
        /// <param name="command">Pass command to call the handle method of command handler</param>
        /// <param name="metadata">Pass meta-data to call the handle method of command handler</param>
        /// <returns>It returns the result of handle method</returns>
        Task<Result<TResult>> DispatchAsync<TMetadata, TResult>(ICommand<TResult> command, TMetadata metadata);

        /// <summary>
        /// Get result based on command handler
        /// </summary>
        /// <typeparam name="TCommand">Pass command type class which inherits ICommand</typeparam>
        /// <typeparam name="TMetadata">Pass metadata as type</typeparam>
        /// <typeparam name="TResult">Pass return type of handle method</typeparam>
        /// <param name="object">Pass model as a parameter which is used for map with command and command will pass to command handler</param>
        /// <param name="metadata">Pass metadata as a parameter and it is directly pass to the handler</param>
        /// <returns>It returns the result of handle method</returns>
        Task<Result<TResult>> DispatchAsync<TCommand, TMetadata, TResult>(object model, TMetadata metadata) where TCommand : ICommand<TResult>;
    }
}
