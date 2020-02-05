using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Chassis.Command.Interfaces
{
    /// <summary>
    /// Implement this interface in handler
    /// </summary>
    /// <typeparam name="TCommand">Pass command type class which inherits ICommand</typeparam>
    /// <typeparam name="TResult">Pass return type of handle method</typeparam>
    public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        /// <summary>
        /// Get result of the handle method
        /// </summary>
        /// <param name="command">Pass command as a parameter</param>
        /// <returns>It returns the result of type IResult</returns>
        Task<Result<TResult>> HandleAsync(TCommand command);
    }

    /// <summary>
    /// Implement this interface in handler
    /// </summary>
    /// <typeparam name="TCommand">Pass command type class which inherits ICommand</typeparam>
    /// <typeparam name="TMetadata">Pass meta-data as a type, meta-data should be any type of class,parameters</typeparam>
    /// <typeparam name="TResult">Pass return type of handle method</typeparam>
    public interface ICommandHandler<TCommand, TMetadata, TResult> where TCommand : ICommand<TResult>
    {
        /// <summary>
        /// Get result of the handle method
        /// </summary>
        /// <param name="command">Pass command type parameter</param>
        /// <param name="metadata">Pass meta-data type parameter</param>
        /// <returns>It returns the result of type IResult</returns>
        Task<Result<TResult>> HandleAsync(TCommand command, TMetadata metadata);
    }
}
