using EMP.Management.AppServices;
using EMP.Management.AppServices.Interface;
using EMP.Management.Command.Interface;
using EMP.Management.Command.Validation;
using EMP.Management.Infrastructure.Interface.Repositories;
using EMP.Management.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ARI.EVOS.Dealer.AppServices.Extension
{
    /// <summary>
    /// Extension method for app service resolver
    /// </summary>
    public static class AppServicesExtenstion
    {
        public static void AddDealerServices(this IServiceCollection services)
        {
            // Command & Repository

            // Query & Command
            services.AddTransient<IEmployee, Employee>();
            services.AddTransient<IMasterData, MasterData>();

            // Repository
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IEmployeeUnitOfWork, EmployeeUnitOfWork>();
            services.AddTransient<IEmployeeValidation,EmployeeValidation>();
        }
    }
}
