using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using Chassis.RegisterServices.Attributes;

namespace Chassis.Repository.NHibernate.Factory
{
    /// <summary>
    /// it is use for create a object for NHibernate Context
    /// </summary>
    [Service(ServiceLifetime.Singleton)]
    public class NHibernateSessionFactory
    {
        private readonly ISessionFactory _sessionfactory;

        /// <summary>
        /// Initializes NHibernate Session Factory
        /// </summary>
        /// <param name="sessionfactory">SessionFactory object</param>
        public NHibernateSessionFactory(ISessionFactory sessionfactory)
        {
            _sessionfactory = sessionfactory;
        }

        /// <summary>
        /// It is use for create a object for NHibernate Context
        /// </summary>
        /// <returns>context for NHibernatContect class</returns>
        public IContext CreateNHibernateContext()
        {
            return new NHibernateContext(_sessionfactory.OpenSession());
        }
    }
}
