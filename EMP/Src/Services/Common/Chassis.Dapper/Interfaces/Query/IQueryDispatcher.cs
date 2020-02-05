using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Chassis.Query.Interfaces;
using CSharpFunctionalExtensions;

namespace Chassis.Dapper.Interfaces.Query
{
    public interface IQueryDispatcher
    {
        Task<Maybe<T1>> DispatchAsync<T1>(IQuery<T1> query, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text);

        Task<(Maybe<T1>, Maybe<T2>)> DispatchAsync<T1, T2>(IQuery<T1, T2> query, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text);

        Task<(Maybe<T1>, Maybe<T2>, Maybe<T3>)> DispatchAsync<T1, T2, T3>(IQuery<T1, T2, T3> query, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text);

        Task<(Maybe<T1>, Maybe<T2>, Maybe<T3>, Maybe<T4>)> DispatchAsync<T1, T2, T3, T4>(IQuery<T1, T2, T3, T4> query, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text);

        Task<(Maybe<T1>, Maybe<T2>, Maybe<T3>, Maybe<T4>, Maybe<T5>)> DispatchAsync<T1, T2, T3, T4, T5>(IQuery<T1, T2, T3, T4, T5> query, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text);

        TQuery CreateQuery<TQuery>(object model);

        Task<Maybe<IEnumerable<TReturn>>> DispatchAsync<TFirst, TSecond, TReturn>
        (Func<TFirst, TSecond, TReturn> func, IQuery<TReturn> query, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType commandType = CommandType.Text) where TReturn : class;
    }
}
