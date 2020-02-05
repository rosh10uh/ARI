using Chassis.Dapper.Interfaces;
using System.Data;
//using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;

namespace Chassis.Dapper.Utility
{
    /// <summary>
    /// Implementation for Configuring Connection
    /// </summary>
    internal class Connection : IConnection
    {
        /// <summary>
        /// Takes Connection string 
        /// </summary>
        public ConnectionString ConnectionString { get; set; }

        /// <summary>
        /// Initializes connection object
        /// </summary>
        /// <param name="connectionString"></param>
        public Connection(ConnectionString connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// Used to get connection from connection string
        /// </summary>
        /// <returns>iDbconnection type object</returns>
        public IDbConnection GetConnection()
        {
            //return new SqlConnection(ConnectionString.Value);
            return new OracleConnection(ConnectionString.Value);
        }
    }
}
