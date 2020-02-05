using System.Data;

namespace Chassis.Dapper.Interfaces
{
    /// <summary>
    /// Interface for Configuring Connection
    /// </summary>
    internal interface IConnection
    {
        /// <summary>
        /// Used to get connection from connection string
        /// </summary>
        /// <returns>iDbconnection type object</returns>
        IDbConnection GetConnection();
    }
}
