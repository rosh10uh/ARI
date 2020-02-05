using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Chassis.Query.Interfaces;
using CSharpFunctionalExtensions;

namespace Chassis.Dapper.Services
{
    public class QueryHandler<TQuery, T1> : BaseQueryHandler<TQuery>, IQueryHandler<TQuery, T1>
                             where TQuery : IQuery<T1>
    {
        public virtual Maybe<T1> Handle(TQuery query, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            Type[] types = GetTypes(typeof(T1));
            var result = QueryExecuter.Execute(types[0], QueryInfo.QueryModels, query, buffered, commandTimeout, commandType);
            MapperConfigurationFactory.CreateMap(result.GetType(), types[0]);
            return MapperConfigurationFactory.Map<T1>(result.Value);
        }

        public virtual Maybe<IEnumerable<TResult>> Handle<TFirst, TSecond, TResult>(Func<TFirst, TSecond, TResult> func, TQuery query, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            return QueryExecuter.Execute<TFirst, TSecond, TResult>(QueryInfo.QueryModels[0], func, query, buffered, splitOn, commandTimeout, commandType);
        }
    }

    public class QueryHandler<TQuery, T1, T2> : BaseQueryHandler<TQuery>, IQueryHandler<TQuery, T1, T2>
                                 where TQuery : IQuery<T1, T2>
    {
        public virtual (Maybe<T1>, Maybe<T2>) Handle(TQuery query, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            Type[] returnTypes = { typeof(T1), typeof(T2) };
            var result = GetMapperConfiguration(x => QueryExecuter.QueryMultiple(x, QueryInfo.QueryModels, query, buffered, commandTimeout, commandType), returnTypes);

            return (MapperConfigurationFactory.Map<T1>(result.ElementAt(0).Value),
                    MapperConfigurationFactory.Map<T2>(result.ElementAt(1).Value));
        }
    }

    public class QueryHandler<TQuery, T1, T2, T3> : BaseQueryHandler<TQuery>, IQueryHandler<TQuery, T1, T2, T3>
                                     where TQuery : IQuery<T1, T2, T3>
    {
        public (Maybe<T1>, Maybe<T2>, Maybe<T3>) Handle(TQuery query, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            Type[] returnTypes = { typeof(T1), typeof(T2), typeof(T3) };
            var result = GetMapperConfiguration(x => QueryExecuter.QueryMultiple(x, QueryInfo.QueryModels, query, buffered, commandTimeout, commandType), returnTypes);

            return (MapperConfigurationFactory.Map<T1>(result.ElementAt(0).Value),
                    MapperConfigurationFactory.Map<T2>(result.ElementAt(1).Value),
                    MapperConfigurationFactory.Map<T3>(result.ElementAt(2).Value));
        }
    }

    public class QueryHandler<TQuery, T1, T2, T3, T4> : BaseQueryHandler<TQuery>, IQueryHandler<TQuery, T1, T2, T3, T4>
                                         where TQuery : IQuery<T1, T2, T3, T4>
    {
        public (Maybe<T1>, Maybe<T2>, Maybe<T3>, Maybe<T4>) Handle(TQuery query, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            Type[] returnTypes = { typeof(T1), typeof(T2), typeof(T3), typeof(T4) };
            var result = GetMapperConfiguration(x => QueryExecuter.QueryMultiple(x, QueryInfo.QueryModels, query, buffered, commandTimeout, commandType), returnTypes);

            return (MapperConfigurationFactory.Map<T1>(result.ElementAt(0).Value),
                    MapperConfigurationFactory.Map<T2>(result.ElementAt(1).Value),
                    MapperConfigurationFactory.Map<T3>(result.ElementAt(2).Value),
                    MapperConfigurationFactory.Map<T4>(result.ElementAt(3).Value));
        }
    }

    public class QueryHandler<TQuery, T1, T2, T3, T4, T5> : BaseQueryHandler<TQuery>, IQueryHandler<TQuery, T1, T2, T3, T4, T5>
                                             where TQuery : IQuery<T1, T2, T3, T4, T5>
    {
        public (Maybe<T1>, Maybe<T2>, Maybe<T3>, Maybe<T4>, Maybe<T5>) Handle(TQuery query, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            Type[] returnTypes = { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) };
            var result = GetMapperConfiguration(x => QueryExecuter.QueryMultiple(x, QueryInfo.QueryModels, query, buffered, commandTimeout, commandType), returnTypes);

            return (MapperConfigurationFactory.Map<T1>(result.ElementAt(0).Value),
                    MapperConfigurationFactory.Map<T2>(result.ElementAt(1).Value),
                    MapperConfigurationFactory.Map<T3>(result.ElementAt(2).Value),
                    MapperConfigurationFactory.Map<T4>(result.ElementAt(3).Value),
                    MapperConfigurationFactory.Map<T5>(result.ElementAt(3).Value));
        }
    }
}
