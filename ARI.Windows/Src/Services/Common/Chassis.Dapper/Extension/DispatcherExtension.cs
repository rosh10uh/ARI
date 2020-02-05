using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using OneRPP.Chassis.Query;
using System;
using System.Threading.Tasks;

namespace OneRPP.Chassis.Dapper
{
    public static class QueryDispatcherExtension
    {
        public static Task<Maybe<T1>> DispatchAsync<T1>(this IQueryDispatcher queryDispatcher, IQuery<T1> query)
        {
        //    QueryHandler<IQuery<T1>, T1> queryHandler = new QueryHandler<IQuery<T1>, T1>();
            return GetHandler(queryDispatcher, query, typeof(QueryHandler<,>),
                              query.GetType(), typeof(T1));
        }

        public static (Maybe<T1>, Maybe<T2>) DispatchAsync<T1, T2>(this IQueryDispatcher queryDispatcher, IQuery<T1, T2> query)
        {
            return GetHandler(queryDispatcher, query, typeof(QueryHandler<,,>),
                              query.GetType(), typeof(T1), typeof(T2));
        }

        public static (Maybe<T1>, Maybe<T2>, Maybe<T3>) DispatchAsync<T1, T2, T3>
                       (this IQueryDispatcher queryDispatcher, IQuery<T1, T2, T3> query)
        {
            return GetHandler(queryDispatcher, query, typeof(QueryHandler<,,,>),
                             query.GetType(), typeof(T1), typeof(T2), typeof(T3));
        }

        public static (Maybe<T1>, Maybe<T2>, Maybe<T3>, Maybe<T4>) DispatchAsync<T1, T2, T3, T4>
                       (this IQueryDispatcher queryDispatcher, IQuery<T1, T2, T3, T4> query)
        {
            return GetHandler(queryDispatcher, query, typeof(QueryHandler<,,,,>),
                             query.GetType(), typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        public static (Maybe<T1>, Maybe<T2>, Maybe<T3>, Maybe<T4>, Maybe<T5>) DispatchAsync<T1, T2, T3, T4, T5>
                     (this IQueryDispatcher queryDispatcher, IQuery<T1, T2, T3, T4, T5> query)
        {

            return GetHandler(queryDispatcher, query, typeof(QueryHandler<,,,,,>),
                             query.GetType(), typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
        }

        private static dynamic GetHandler(IQueryDispatcher queryDispatcher, object query, Type type, params Type[] arg)
        {
            Type handlerType = type.MakeGenericType(arg);
            dynamic handler = queryDispatcher.ServiceProvider.GetService(handlerType);
            if (handler == null)
            {
                handler = ActivatorUtilities.CreateInstance(queryDispatcher.ServiceProvider, handlerType);
            }

            return handler.Handle((dynamic)query);
        }
    }
}
