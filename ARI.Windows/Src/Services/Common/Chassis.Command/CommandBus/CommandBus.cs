using System;
using System.Threading.Tasks;
using AutoMapper;
using Chassis.Command.Interfaces;
using Chassis.RegisterServices.Attributes;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;

namespace Chassis.Command.CommandBus
{
    /// <summary>
    /// Used to pass command and Metadata to command handler, map DTO to command
    /// </summary>
    [Service]
    public sealed class CommandBus : ICommandBus
    {
        /// <summary>
        /// Get instance at runtime
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the service provider
        /// </summary>
        public CommandBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Get result based on command handler
        /// </summary>
        /// <typeparam name="TCommand">Pass command type class which inherits ICommand</typeparam>
        /// <param name="command">Pass command to call the handle method of command handler</param>
        /// <returns>It returns the result of handle method</returns>
        public Task<Result<TResult>> DispatchAsync<TResult>(ICommand<TResult> command)
        {
            dynamic handler = GetHandler<TResult>(command.GetType());
            return handler.HandleAsync((dynamic)command);
        }

        /// <summary>
        /// Get result based on command handler
        /// </summary>
        /// <typeparam name="TCommand">Used to pass command type class which inherits ICommand</typeparam>
        /// <typeparam name="TModel">Used to pass DTO</typeparam>
        /// <param name="model">Pass model to map with command and command will pass to the handler</param>
        /// <returns>It returns the result of handle method</returns>
        public Task<Result<TResult>> DispatchAsync<TCommand, TResult>(object model) where TCommand : ICommand<TResult>
        {
            var mapper = _serviceProvider.GetRequiredService<IMapper>();
            var command = mapper.Map<TCommand>(model);
            dynamic handler = GetHandler<TResult>(typeof(TCommand));
            return handler.HandleAsync((dynamic)command);
        }

        /// <summary>
        /// Get result based on command handler
        /// </summary>
        /// <typeparam name="TCommand">Pass command type class which inherits ICommand</typeparam>
        /// <typeparam name="TMetadata">Pass meta-data as a type, Metadata should be any type of class,parameters</typeparam>
        /// <param name="command">Pass command to call the handle method of command handler</param>
        /// <param name="metadata">Pass meta-data to call the handle method of command handler</param>
        /// <returns>It returns the result of handle method</returns>
        public Task<Result<TResult>> DispatchAsync<TMetadata, TResult>(ICommand<TResult> command, TMetadata metadata)
        {
            dynamic handler = GetHandlerWithMetadata<TMetadata, TResult>(command.GetType());
            return handler.HandleAsync((dynamic)command, (dynamic)metadata);
        }

        /// <summary>
        /// Get result based on command handler
        /// </summary>
        /// <typeparam name="TCommand">Pass command type class which inherits ICommand</typeparam>
        /// <typeparam name="TModel">Pass DTO as type</typeparam>
        /// <typeparam name="TMetadata">Pass Metadata as type</typeparam>
        /// <param name="model">Pass model as a parameter which is used for map with command and command will pass to command handler</param>
        /// <param name="metadata">Pass Metadata as a parameter and it is directly pass to the handler</param>
        /// <returns>It returns the result of handle method</returns>
        public Task<Result<TResult>> DispatchAsync<TCommand, TMetadata, TResult>(object model, TMetadata metadata) where TCommand : ICommand<TResult>
        {
            var mapper = _serviceProvider.GetRequiredService<IMapper>();
            var command = mapper.Map<TCommand>(model);
            dynamic handler = GetHandlerWithMetadata<TMetadata, TResult>(typeof(TCommand));
            return handler.HandleAsync((dynamic)command, metadata);
        }

        /// <summary>
        /// Get instance of command handler at run time and call relevant handle method based on its type and parameters
        /// </summary>
        /// <typeparam name="TCommand">Pass command type class which inherits the ICommand</typeparam>
        /// <returns>It returns the command handler object which is used for call handle method of command handler</returns>
        private dynamic GetHandler<TResult>(Type commandType)
        {
            Type type = typeof(ICommandHandler<,>);
            return GetHandler(type, commandType, typeof(TResult));
        }

        /// <summary>
        /// Get instance of command handler at run time and call relevant handle method based on its type and parameters
        /// </summary>
        /// <typeparam name="TCommand">Pass command as a type class which inherits the ICommand</typeparam>
        /// <typeparam name="TMetadata">Pass Metadata as a type, Metadata would be class or a parameters</typeparam>
        /// <returns>It returns the command handler object which is used for call handle method of command handler</returns>
        private dynamic GetHandlerWithMetadata<TMetadata, TResult>(Type commandType)
        {
            Type type = typeof(ICommandHandler<,,>);
            return GetHandler(type, commandType, typeof(TMetadata), typeof(TResult));
        }

        /// <summary>
        /// Get instance of command handler at run time
        /// </summary>
        /// <param name="type">Pass type of command handler</param>
        /// <param name="types">Pass type of command,Metadata</param>
        /// <returns>It returns the command handler object which is used for call handle method of command handler</returns>
        private dynamic GetHandler(Type type, params Type[] types)
        {
            Type handlerType = type.MakeGenericType(types);
            var res = _serviceProvider.GetService(handlerType);

            return res;
        }
    }
}
