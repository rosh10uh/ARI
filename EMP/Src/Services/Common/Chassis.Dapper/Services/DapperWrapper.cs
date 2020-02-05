using Dapper;
using Chassis.Dapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using static Dapper.SqlMapper;

namespace Chassis.Dapper.Services
{
    /// <summary>
    /// Implementation for Dapper Encapsulation
    /// </summary>
    internal class DapperWrapper : IDapperWrapper
    {
        /// <summary>
        /// Connection object
        /// </summary>
        private readonly IConnection _connection;

        /// <summary>
        /// Initializes dapper Wrapper object
        /// </summary>
        /// <param name="connection"></param>
        public DapperWrapper(IConnection connection)
        {
            _connection = connection;
        }

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
        public IEnumerable<T> Query<T>(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection dbConnection = _connection.GetConnection())
            {
                return dbConnection.Query<T>(sql, param, buffered: buffered, commandTimeout: commandTimeout, commandType: commandType);
            }
        }
        public T QueryFirst<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (IDbConnection dbConnection = _connection.GetConnection())
            {
                return dbConnection.QueryFirst<T>(sql, param, commandTimeout: commandTimeout, commandType: commandType);
            }
        }

        public IEnumerable<object> Query(Type type, string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection dbConnection = _connection.GetConnection())
            {
                return dbConnection.Query(type, sql, param, buffered: buffered, commandTimeout: commandTimeout, commandType: commandType);
            }
        }

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
        public IEnumerable<T> Query<TFirst, TSecond, T>(string sql, Func<TFirst, TSecond, T> map, object param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection dbConnection = _connection.GetConnection())
            {
                return dbConnection.Query(sql, map, param, buffered: buffered, splitOn: splitOn, commandTimeout: commandTimeout, commandType: commandType);
            }
        }

        /// <summary>
        /// Take two sql queries and returns multiple result
        /// </summary>
        /// <typeparam name="T1">Result Type 1</typeparam>
        /// <typeparam name="T2">Result Type 2</typeparam>
        /// <param name="sql">Resource name</param>
        /// <param name="param">Parameters</param>
        /// <param name="dbTransaction">Transaction object</param>
        /// <param name="commandTimeout">command time out value</param>
        /// <param name="commandType"> Command Type</param>
        /// <returns>Result with two objects</returns>
        public (IEnumerable<T1>, IEnumerable<T2>) QueryMultiple<T1, T2>(string sql, object param = null, IDbTransaction dbTransaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            (IEnumerable<T1>, IEnumerable<T2>)
                func(GridReader gridReader) =>
                (
                    gridReader.Read<T1>(),
                    gridReader.Read<T2>()
                );
            return Execute(func, sql, param, commandTimeout, commandType);
        }

        public IEnumerable<IEnumerable<object>> QueryMultiple(Type[] type, string sql, object param = null, bool bufferred = true, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            IEnumerable<IEnumerable<object>> func(GridReader gridReader) => QueryReader(type, gridReader, bufferred);
            return Execute(func, sql, param, commandTimeout, commandType);
        }

        private IEnumerable<IEnumerable<object>> QueryReader(Type[] types, GridReader gridReader, bool bufferred = true)
        {
            IList<IEnumerable<object>> results = new List<IEnumerable<object>>();
            foreach (var type in types)
            {
                results.Add(gridReader.Read(type, bufferred));
                //close the reader 
                // yield return gridReader.Read(item);
            }

            return results;
        }

        /// <summary>
        /// Take three sql queries and returns multiple result
        /// </summary>
        /// <typeparam name="T1">Result Type 1</typeparam>
        /// <typeparam name="T2">Result Type 2</typeparam>
        /// <typeparam name="T3">Result Type 3</typeparam>
        /// <param name="sql">Resource name</param>
        /// <param name="param">Parameters</param>
        /// <param name="dbTransaction">Transaction object</param>
        /// <param name="commandTimeout">Command time out value</param>
        /// <param name="commandType"> Command Type</param>
        /// <returns>Result with three objects</returns>
        public (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>) QueryMultiple<T1, T2, T3>(string sql, object param = null, IDbTransaction dbTransaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>)
                func(GridReader gridReader) =>
                (
                    gridReader.Read<T1>(),
                    gridReader.Read<T2>(),
                    gridReader.Read<T3>()
                );
            return Execute(func, sql, param, commandTimeout, commandType);
        }

        /// <summary>
        /// Take four sql queries and returns multiple result
        /// </summary>
        /// <typeparam name="T1">Result Type 1</typeparam>
        /// <typeparam name="T2">Result Type 2</typeparam>
        /// <typeparam name="T3">Result Type 3</typeparam>
        /// <typeparam name="T4">Result Type 4</typeparam>
        /// <param name="sql">Resource name</param>
        /// <param name="param">Parameters</param>
        /// <param name="dbTransaction">Transaction object</param>
        /// <param name="commandTimeout">Command time out value</param>
        /// <param name="commandType"> Command Type</param>
        /// <returns>Result with four objects</returns>
        public (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>) QueryMultiple<T1, T2, T3, T4>(string sql, object param = null, IDbTransaction dbTransaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>)
                func(GridReader gridReader) =>
                (
                    gridReader.Read<T1>(),
                    gridReader.Read<T2>(),
                    gridReader.Read<T3>(),
                    gridReader.Read<T4>()
                );
            return Execute(func, sql, param, commandTimeout, commandType);
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
        /// <param name="param">Parameters</param>
        /// <param name="dbTransaction">Transaction object</param>
        /// <param name="commandTimeout">Command time out value</param>
        /// <param name="commandType"> Command Type</param>
        /// <returns>Result with five objects</returns>
        public (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>) QueryMultiple<T1, T2, T3, T4, T5>(string sql, object param = null, IDbTransaction dbTransaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            (IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>)
                func(GridReader gridReader) =>
                (
                    gridReader.Read<T1>(),
                    gridReader.Read<T2>(),
                    gridReader.Read<T3>(),
                    gridReader.Read<T4>(),
                    gridReader.Read<T5>()
                );
            return Execute(func, sql, param, commandTimeout, commandType);
        }

        /// <summary>
        /// Executes multiple queries and returns result
        /// </summary>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <param name="func">Gets result from gridreader and sends as TResult</param>
        /// <param name="sql">Resource name</param>
        /// <param name="param">Parameters</param>
        /// <param name="dbTransaction">Transaction object</param>
        /// <param name="commandTimeout">Command time out value</param>
        /// <param name="commandType"> Command Type</param>
        /// <returns>Result with multiple TModels</returns>
        private TResult Execute<TResult>(Func<GridReader, TResult> func, string sql, object param = null, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection dbConnection = _connection.GetConnection())
            {
                var gridReader = dbConnection.QueryMultiple(sql, param, commandTimeout: commandTimeout, commandType: commandType);
                return func(gridReader);
            }
        }

    }
}
