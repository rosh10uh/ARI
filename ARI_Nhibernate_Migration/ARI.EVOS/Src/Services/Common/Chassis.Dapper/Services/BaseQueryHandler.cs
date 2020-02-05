using CSharpFunctionalExtensions;
using Chassis.Dapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Chassis.Dapper.Interfaces.Query;

namespace Chassis.Dapper.Services
{
    public abstract class BaseQueryHandler<TQuery>
    {
        internal IQueryExecuter QueryExecuter { get; private set; }
        internal IQueryInfo<TQuery> QueryInfo { get; private set; }
        internal IMapperConfigurationFactory MapperConfigurationFactory { get; private set; }

        internal void Init(IQueryExecuter queryExecuter, object queryInfo, IMapperConfigurationFactory mapperConfigurationFactory)
        {
            QueryExecuter = queryExecuter;
            QueryInfo = queryInfo as IQueryInfo<TQuery>;
            MapperConfigurationFactory = mapperConfigurationFactory;
        }

        internal Type[] GetTypes(params Type[] types)
        {
            Type type;
            Type[] newTypes = new Type[types.Length];
            for (int typeIndex = 0; typeIndex < types.Length; typeIndex++)
            {
                type = types[typeIndex];
                newTypes[typeIndex] = type.IsGenericType ? type.GetGenericArguments()[0] : type;
            }

            return newTypes;
        }

        internal Maybe<IEnumerable<object>>[] GetMapperConfiguration(Func<Type[], IList<Maybe<IEnumerable<object>>>> queryResult, params Type[] types)
        {
            Type[] returnTypes = GetTypes(types);
            var result = queryResult(returnTypes).ToArray();
            MapperConfigurationFactory.CreateMap(result, types);
            return result;
        }
    }
}
