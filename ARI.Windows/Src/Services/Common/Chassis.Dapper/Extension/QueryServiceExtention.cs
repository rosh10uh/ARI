using Microsoft.Extensions.DependencyInjection;
using Chassis.Dapper.Utility;
using System;
using System.Linq;
using Chassis.Dapper.Interfaces;
using Chassis.Query.Interfaces;

namespace Chassis.Dapper.Extension
{
    internal static class QueryServiceExtension
    {
        internal static void AddQuery(this IServiceCollection services)
        {
            LoadQueryInfo(services);
        }

        //todo: Refactor 
        private static void LoadQueryInfo(IServiceCollection services)
        {
            var handlerTypes = AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(s => s.GetTypes())
                       .Where(x => x.GetInterfaces()
                       .Any(y => IsHandlerInterface(y)))
                       .Where(x => !x.IsInterface);

            Type type = typeof(IQueryInfo<>);
            Type typeReg = typeof(QueryInfo<>);

            foreach (var item in handlerTypes)
            {
                Type handlerType = type.MakeGenericType(item);
                Type handlerTypeReg = typeReg.MakeGenericType(item);
                var queryInfo = Activator.CreateInstance(handlerTypeReg);
                ((IQueryInfo)queryInfo).Init();

                services.AddSingleton(handlerType, queryInfo);
            }
        }

        private static bool IsHandlerInterface(Type type)
        {
            if (!type.IsGenericType)
                return false;

            Type typeDefinition = type.GetGenericTypeDefinition();
            return typeDefinition == typeof(IQuery<>) ||
                   typeDefinition == typeof(IQuery<,>) ||
                   typeDefinition == typeof(IQuery<,,>) ||
                   typeDefinition == typeof(IQuery<,,,>) ||
                   typeDefinition == typeof(IQuery<,,,,>);
        }
    }
}
