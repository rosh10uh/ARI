using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AutoMapper;
using Chassis.Dapper.Interfaces;
using Chassis.Dapper.Interfaces.Query;
using Chassis.Dapper.Services;
using Chassis.Query.Interfaces;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;

namespace Chassis.Dapper.Utility
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<Maybe<T1>> DispatchAsync<T1>(IQuery<T1> query, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            return GetHandler(query, typeof(QueryHandler<,>), buffered, commandTimeout, commandType,
                              query.GetType(), typeof(T1));
        }

        public async Task<(Maybe<T1>, Maybe<T2>)> DispatchAsync<T1, T2>(IQuery<T1, T2> query, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            return await GetHandler(query, typeof(QueryHandler<,,>), buffered, commandTimeout, commandType,
                              query.GetType(), typeof(T1), typeof(T2));
        }

        public async Task<(Maybe<T1>, Maybe<T2>, Maybe<T3>)> DispatchAsync<T1, T2, T3>(IQuery<T1, T2, T3> query, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            return await GetHandler(query, typeof(QueryHandler<,,,>), buffered, commandTimeout, commandType,
                             query.GetType(), typeof(T1), typeof(T2), typeof(T3));
        }

        public async Task<(Maybe<T1>, Maybe<T2>, Maybe<T3>, Maybe<T4>)> DispatchAsync<T1, T2, T3, T4>(IQuery<T1, T2, T3, T4> query, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            return await GetHandler(query, typeof(QueryHandler<,,,,>), buffered, commandTimeout, commandType,
                             query.GetType(), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        public async Task<(Maybe<T1>, Maybe<T2>, Maybe<T3>, Maybe<T4>, Maybe<T5>)> DispatchAsync<T1, T2, T3, T4, T5>(IQuery<T1, T2, T3, T4, T5> query, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            return await GetHandler(query, typeof(QueryHandler<,,,,,>), buffered, commandTimeout, commandType,
                             query.GetType(), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
        }

        public Task<Maybe<IEnumerable<TReturn>>> DispatchAsync<TFirst, TSecond, TReturn>
                (Func<TFirst, TSecond, TReturn> func, IQuery<TReturn> query, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType commandType = CommandType.Text) where TReturn : class
        {
            dynamic handler = InitializeQueryHandler(typeof(QueryHandler<,>), query.GetType(), typeof(TReturn));
            return Task.FromResult(handler.Handle(func, (dynamic)query, buffered, splitOn, commandTimeout, commandType));
        }

        public TQuery CreateQuery<TQuery>(object model)
        {
            return Mapper.Map<TQuery>(model);
        }

        private dynamic GetHandler(object query, Type type, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text, params Type[] typeArguments)
        {
            dynamic handler = InitializeQueryHandler(type, typeArguments);
            return Task.FromResult(handler.Handle((dynamic)query, buffered, commandTimeout, commandType));
        }

        private dynamic InitializeQueryHandler(Type type, params Type[] typeArguments)
        {
            Type handlerType = type.MakeGenericType(typeArguments);
            dynamic handler = _serviceProvider.GetService(handlerType);
            if (handler == null)
            {
                handler = ActivatorUtilities.CreateInstance(_serviceProvider, handlerType);
            }

            var queryExecuter = _serviceProvider.GetService<IQueryExecuter>();
            var mapperConfigurationFactory = _serviceProvider.GetService<IMapperConfigurationFactory>();
            var queryInfo = _serviceProvider.GetService(typeof(IQueryInfo<>).MakeGenericType(typeArguments[0]));
            handler.Init(queryExecuter, queryInfo, mapperConfigurationFactory);

            return handler;
        }

        private IMapper Mapper => _serviceProvider.GetService<IMapper>();

    }
}
