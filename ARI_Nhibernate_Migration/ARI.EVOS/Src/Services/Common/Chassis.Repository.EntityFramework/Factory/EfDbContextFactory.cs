using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Chassis.Repository.EntityFramework.Factory
{
    /// <summary>
    /// This class is use to create object of DbContext
    /// </summary>
    internal class EfDbContextFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Parameter rise constructor with IServiceProvider parameter
        /// </summary>
        /// <param name="serviceProvider"></param>
        public EfDbContextFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        ///  Create object for DbContext
        /// </summary>
        /// <typeparam name="T">GenericDbContext</typeparam>
        /// <param name="action">Action<ModelBuilder></param>
        /// <returns>DbContext</returns>
        internal DbContext CreateEfContext<T>(Action<ModelBuilder> action) where T : GenericDbContext
        {
            var dbContextOption = _serviceProvider.GetService<DbContextOptionsBuilder<T>>();
            var efContext = (T)ActivatorUtilities.CreateInstance(_serviceProvider, typeof(T), new object[] { dbContextOption.Options });
            efContext.OnEfModelBuilder += new EfModelBuilder(x => action.Invoke(x));
            return efContext;
        }
    }
}
