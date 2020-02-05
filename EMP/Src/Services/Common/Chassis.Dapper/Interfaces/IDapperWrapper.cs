using System;
using System.Collections.Generic;
using System.Data;

namespace Chassis.Dapper.Interfaces
{
    /// <summary>
    /// Interface for Dapper Encapsulation
    /// </summary>
    internal interface IDapperWrapper
    {
        /// <summary>
        /// Calls dapper method for executing query
        /// </summary>
        /// <typeparam name="T">Return type</typeparam>
        /// <param name="sql">sql query</param>
        /// <param name="param">Parameters</param>
        /// <param name="buffered">Buffered</param>
        /// <param name="commandTimeout">Command time out value</param>
        /// <param name="commandType"> Command Type</param>
        /// <returns>List of T</returns>
        IEnumerable<T> Query<T>(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text);

        IEnumerable<object> Query(Type type, string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text);

        IEnumerable<IEnumerable<object>> QueryMultiple(Type[] type, string sql, object param = null, bool bufferred = true, int? commandTimeout = null, CommandType commandType = CommandType.Text);

        T QueryFirst<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Calls dapper method for executing query and returning nested model
        /// </summary>
        /// <typeparam name="TFirst">Parent model</typeparam>
        /// <typeparam name="TSecond">Child model</typeparam>
        /// <typeparam name="TReturn">Actual return type</typeparam>
        /// <param name="sql">Sql query</param>
        /// <param name="map">Action method to map query output to model</param>
        /// <param name="param">parameters</param>
        /// <param name="buffered">buffer</param>
        /// <param name="splitOn">Split on field name</param>
        /// <param name="commandTimeout">Command time out value</param>
        /// <param name="commandType">command type</param>
        /// <returns>List of TReturn</returns>
        IEnumerable<T> Query<TFirst, TSecond, T>(string sql, Func<TFirst, TSecond, T> map, object param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType commandType = CommandType.Text);

        /// <summary>
        /// Take two sql queries and returns multiple result
        /// </summary>
        /// <typeparam name="T1">Result Type 1</typeparam>
        /// <typeparam name="T2">Result Type 2</typeparam>
        /// <param name="sql">Resource name</param>
        /// <param name="param">Paramters</param>
        /// <param name="dbTransaction">Transaction object</param>
        /// <param name="commandTimeout">Command time out value</param>
        /// <param name="commandType"> Command Type</param>
        /// <returns>Result with two objects</returns>
        (IEnumerable<T1>, IEnumerable<T2>) QueryMultiple<T1, T2>(string sql, object param = null, IDbTransaction dbTransaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text);

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
        (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>) QueryMultiple<T1, T2, T3>(string sql, object param = null, IDbTransaction dbTransaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text);

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
        (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>) QueryMultiple<T1, T2, T3, T4>(string sql, object param = null, IDbTransaction dbTransaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text);

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
        (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>) QueryMultiple<T1, T2, T3, T4, T5>(string sql, object param = null, IDbTransaction dbTransaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text);
    }
}
