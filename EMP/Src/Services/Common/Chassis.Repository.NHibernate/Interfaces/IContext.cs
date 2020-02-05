using NHibernate;
using System;

namespace Chassis.Repository
{
    /// <summary>
    /// Context interface.
    /// </summary>
    public interface IContext : IDisposable
    {
        ISession Session { get; }
    }
}
