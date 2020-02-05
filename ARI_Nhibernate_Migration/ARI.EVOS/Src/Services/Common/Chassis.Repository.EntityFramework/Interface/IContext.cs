using System;
using Microsoft.EntityFrameworkCore;

namespace Chassis.Repository.EntityFramework.Interface
{
    /// <summary>
    /// Context interface.
    /// </summary>
    public interface IContext : IDisposable
    {
        DbContext DbContext { get; }
    }
}
