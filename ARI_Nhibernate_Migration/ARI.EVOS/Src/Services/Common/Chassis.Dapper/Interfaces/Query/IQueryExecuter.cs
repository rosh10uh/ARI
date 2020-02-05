using System;
using System.Collections.Generic;
using System.Data;
using Chassis.Dapper.Utility;
using CSharpFunctionalExtensions;

namespace Chassis.Dapper.Interfaces.Query
{
    /// <summary>
    /// Interface for query executer
    /// </summary>
    internal interface IQueryExecuter
    {
        /// <summary>
        /// Takes sql resource and parameters and returns List of TReturn
        /// </summary>
        /// <typeparam name="TReturn">Return Type</typeparam>
        /// <param name="sql">SQL resource name</param>
        /// <param name="param">Parameters</param>
        /// <param name="buffered">Buffer</param>
        /// <param name="commandTimeout">Command time out value</param>
        /// <returns>List of TReturn</returns>
        Maybe<IEnumerable<TReturn>> Execute<TReturn>(QueryModel queryModel, object param = null, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text);

        Maybe<IEnumerable<object>> Execute(Type type, QueryModel[] queryModels, object param = null, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text);

        IList<Maybe<IEnumerable<object>>> QueryMultiple(Type[] types, QueryModel[] queryModels, object param = null, bool bufferred = true, int? commandTimeout = null, CommandType commandType = CommandType.Text);

        Maybe<TReturn> ExecuteFirst<TReturn>(QueryModel queryModel, object param = null, int? commandTimeout = null, CommandType commandType = CommandType.Text);

        /// <summary>
        /// Takes sql query and returns nested model
        /// </summary>
        /// <typeparam name="TFirst">Outer model type</typeparam>
        /// <typeparam name="TSecond">Inner model type</typeparam>
        /// <typeparam name="TReturn">Returning model type</typeparam>
        /// <param name="sql">SQL resource name</param>
        /// <param name="map">Mapping action</param>
        /// <param name="param">Parameters</param>
        /// <param name="buffered">buffer</param>
        /// <param name="splitOn">Split on field name</param>
        /// <param name="commandTimeout">command time out value</param>
        /// <returns>List of TReturn</returns>
        Maybe<IEnumerable<TReturn>> Execute<TFirst, TSecond, TReturn>(QueryModel queryModel, Func<TFirst, TSecond, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType commandType = CommandType.Text);

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
        (Maybe<IEnumerable<T1>>, Maybe<IEnumerable<T2>>) ExecuteMultiple<T1, T2>(QueryModel[] queryModels, object param = null, IDbTransaction dbTransaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text);

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
        (Maybe<IEnumerable<T1>>, Maybe<IEnumerable<T2>>, Maybe<IEnumerable<T3>>) ExecuteMultiple<T1, T2, T3>(QueryModel[] queryModels, object param = null, IDbTransaction dbTransaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text);

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
        (Maybe<IEnumerable<T1>>, Maybe<IEnumerable<T2>>, Maybe<IEnumerable<T3>>, Maybe<IEnumerable<T4>>) ExecuteMultiple<T1, T2, T3, T4>(QueryModel[] queryModels, object param = null, IDbTransaction dbTransaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text);


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
        (Maybe<IEnumerable<T1>>, Maybe<IEnumerable<T2>>, Maybe<IEnumerable<T3>>, Maybe<IEnumerable<T4>>, Maybe<IEnumerable<T5>>) ExecuteMultiple<T1, T2, T3, T4, T5>(QueryModel[] queryModels, object param = null, IDbTransaction dbTransaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text);
    }
}
