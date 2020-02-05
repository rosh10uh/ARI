using CSharpFunctionalExtensions;
using Chassis.Dapper.Interfaces;
using Chassis.Dapper.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Chassis.Dapper.Interfaces.Query;

namespace Chassis.Dapper.Services
{
    /// <summary>
    /// Implementation for query executer
    /// </summary>
    internal class QueryExecuter : IQueryExecuter
    {
        /// <summary>
        /// Dapper Wrapper
        /// </summary>
        private readonly IDapperWrapper _dapperWrapper;
        private readonly ISqlQuery _sqlQuery;

        /// <summary>
        /// Initializes Query Executor object
        /// </summary>
        /// <param name="resourceReader">Resource Reader</param>
        /// <param name="dapperWrapper">dapper wrapper</param>
        /// <param name="razor">Razor </param>
        public QueryExecuter(IDapperWrapper dapperWrapper, ISqlQuery sqlQuery)
        {
            _dapperWrapper = dapperWrapper;
            _sqlQuery = sqlQuery;
        }

        /// <summary>
        /// Takes sql resource and parameters and returns List of TReturn
        /// </summary>
        /// <typeparam name="TReturn">Return model</typeparam>
        /// <param name="sql">SQL resource name</param>
        /// <param name="param">parameters</param>
        /// <param name="buffered">buffer</param>
        /// <param name="commandTimeout">command time out value</param>
        /// <param name="commandType"> Command Type</param>
        /// <returns>List of TReturn</returns>
        public Maybe<IEnumerable<TReturn>> Execute<TReturn>(QueryModel queryModel, object param = null, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            string query = _sqlQuery.GetQuery(param, queryModel);
            var result = _dapperWrapper.Query<TReturn>(query, param, buffered, commandTimeout, commandType);
            return MaybeOf(result);
        }

        public Maybe<TReturn> ExecuteFirst<TReturn>(QueryModel queryModel, object param = null, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            string query = _sqlQuery.GetQuery(param, queryModel);
            var result = _dapperWrapper.QueryFirst<TReturn>(query, param, commandTimeout, commandType);
            return MaybeOf(result);
        }

        /// <summary>
        /// Takes sql query and returns nested model
        /// </summary>
        /// <typeparam name="TFirst">Outer model type</typeparam>
        /// <typeparam name="TSecond">Inner model type</typeparam>
        /// <typeparam name="TReturn">Returning model type</typeparam>
        /// <param name="sql">SQL resource name</param>
        /// <param name="map">Mapping action</param>
        /// <param name="param">Parameters</param>
        /// <param name="buffered">Buffer</param>
        /// <param name="splitOn">Split on field name</param>
        /// <param name="commandTimeout">Command time out value</param>
        /// <param name="commandType"> Command Type</param>
        /// <returns>List of TReturn</returns>
        public Maybe<IEnumerable<TReturn>> Execute<TFirst, TSecond, TReturn>(QueryModel queryModel, Func<TFirst, TSecond, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            string query = _sqlQuery.GetQuery(param, queryModel);
            var result = _dapperWrapper.Query(query, map, param, buffered, splitOn, commandTimeout, commandType);
            return MaybeOf(result);
        }

        /// <summary>
        /// Take two sql queries and returns multiple result
        /// </summary>
        /// <typeparam name="T1">Result Type 1</typeparam>
        /// <typeparam name="T2">Result Type 2</typeparam>
        /// <param name="sql">Resource name</param>
        /// <param name="param">Paramters</param>
        /// <param name="dbTransaction">Transaction object</param>
        /// <param name="commandTimeout">command time out value</param>
        /// <param name="commandType"> Command Type</param>
        /// <returns>Result with two objects</returns>
        public (Maybe<IEnumerable<T1>>, Maybe<IEnumerable<T2>>) ExecuteMultiple<T1, T2>(QueryModel[] queryModels, object param = null, IDbTransaction dbTransaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            string query = _sqlQuery.GetQuery(param, queryModels);
            var result = _dapperWrapper.QueryMultiple<T1, T2>(query, param, dbTransaction, commandTimeout, commandType);

            return (MaybeOf(result.Item1),
                    MaybeOf(result.Item2));
        }

        /// <summary>
        /// Take three sql queries and returns multiple result
        /// </summary>
        /// <typeparam name="T1">Result Type 1</typeparam>
        /// <typeparam name="T2">Result Type 2</typeparam>
        /// <typeparam name="T3">Result Type 3</typeparam>
        /// <param name="sql">Resource name</param>
        /// <param name="param">Paramters</param>
        /// <param name="dbTransaction">Transaction object</param>
        /// <param name="commandTimeout">command time out value</param>
        /// <param name="commandType"> Command Type</param>
        /// <returns>Result with three objects</returns>
        public (Maybe<IEnumerable<T1>>, Maybe<IEnumerable<T2>>, Maybe<IEnumerable<T3>>) ExecuteMultiple<T1, T2, T3>(QueryModel[] queryModels, object param = null, IDbTransaction dbTransaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            string query = _sqlQuery.GetQuery(param, queryModels);
            var result = _dapperWrapper.QueryMultiple<T1, T2, T3>(query, param, dbTransaction, commandTimeout, commandType);

            return (MaybeOf(result.Item1),
                    MaybeOf(result.Item2),
                    MaybeOf(result.Item3));
        }

        /// <summary>
        /// Take four sql queries and returns multiple result
        /// </summary>
        /// <typeparam name="T1">Result Type 1</typeparam>
        /// <typeparam name="T2">Result Type 2</typeparam>
        /// <typeparam name="T3">Result Type 3</typeparam>
        /// <typeparam name="T4">Result Type 4</typeparam>
        /// <param name="sql">Resource name</param>
        /// <param name="param">Paramters</param>
        /// <param name="dbTransaction">Transaction object</param>
        /// <param name="commandTimeout">command time out value</param>
        /// <param name="commandType"> Command Type</param>
        /// <returns>Result with four objects</returns>
        public (Maybe<IEnumerable<T1>>, Maybe<IEnumerable<T2>>, Maybe<IEnumerable<T3>>, Maybe<IEnumerable<T4>>) ExecuteMultiple<T1, T2, T3, T4>(QueryModel[] queryModels, object param = null, IDbTransaction dbTransaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            string query = _sqlQuery.GetQuery(param, queryModels);
            var result = _dapperWrapper.QueryMultiple<T1, T2, T3, T4>(query, param, dbTransaction, commandTimeout, commandType);

            return (MaybeOf(result.Item1),
                    MaybeOf(result.Item2),
                    MaybeOf(result.Item3),
                    MaybeOf(result.Item4));
        }

        /// <summary>
        /// Take five sql queries and returns multiple result
        /// </summary>
        /// <typeparam name="T1">Result Type 1</typeparam>
        /// <typeparam name="T2">Result Type 2</typeparam>
        /// <typeparam name="T3">Result Type 3</typeparam>
        /// <typeparam name="T4">Result Type 4</typeparam>
        /// <typeparam name="T5">Result Type 5</typeparam>
        /// <param name="sql">Resource name</param>
        /// <param name="param">Paramters</param>
        /// <param name="dbTransaction">Transaction object</param>
        /// <param name="commandTimeout">command time out value</param>
        /// <param name="commandType"> Command Type</param>
        /// <returns>Result with five objects</returns>
        public (Maybe<IEnumerable<T1>>, Maybe<IEnumerable<T2>>, Maybe<IEnumerable<T3>>, Maybe<IEnumerable<T4>>, Maybe<IEnumerable<T5>>) ExecuteMultiple<T1, T2, T3, T4, T5>(QueryModel[] queryModels, object param = null, IDbTransaction dbTransaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            string query = _sqlQuery.GetQuery(param, queryModels);
            var result = _dapperWrapper.QueryMultiple<T1, T2, T3, T4, T5>(query, param, dbTransaction, commandTimeout, commandType);

            return (MaybeOf(result.Item1),
                    MaybeOf(result.Item2),
                    MaybeOf(result.Item3),
                    MaybeOf(result.Item4),
                    MaybeOf(result.Item5));
        }

        public Maybe<IEnumerable<object>> Execute(Type type, QueryModel[] queryModels, object param = null, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            string query = _sqlQuery.GetQuery(param, queryModels);
            var result = _dapperWrapper.Query(type, query, param, buffered, commandTimeout, commandType);
            return MaybeOf(result);
        }

        public IList<Maybe<IEnumerable<object>>> QueryMultiple(Type[] types, QueryModel[] queryModels, object param = null, bool bufferred = true, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            string query = _sqlQuery.GetQuery(param, queryModels);
            return _dapperWrapper.QueryMultiple(types, query, param, bufferred, commandTimeout, commandType)?.ToList()
                                 .ConvertAll(x => MaybeOf(x));
        }

        private Maybe<IEnumerable<T>> MaybeOf<T>(IEnumerable<T> value)
        {
            return Maybe<IEnumerable<T>>.From(value);
        }

        private Maybe<T> MaybeOf<T>(T value)
        {
            return Maybe<T>.From(value);
        }
    }
}
