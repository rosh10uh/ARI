using ARI.EVOS.Dealer.AppServices.Interface;
using ARI.EVOS.Dealer.Command.Interface;
using ARI.EVOS.Dealer.Command.Validation;
using ARI.EVOS.Dealer.Infrastructure.Interface.Repositories;
using ARI.EVOS.Dealer.Infrastructure.Repositories;
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
            services.AddTransient<IMasterData, MasterData>();
            services.AddTransient<IDealerNetwork, DealerNetwork>();

            // Repository
            services.AddTransient<IDealerRepository, DealerRepository>();
            services.AddTransient<IDealerUnitOfWork, DealerUnitOfWork>();
            services.AddTransient<IDealerValidation, DealerValidation>();

            services.AddTransient<IContactEmailRepository, ContactEmailRepository>();
            services.AddTransient<IContactEmailUnitOfWork, ContactEmailUnitOfWork>();

            services.AddTransient<IGetReadyRepository, GetReadyRepository>();
            services.AddTransient<IGetReadyValidation, GetReadyValidation>();

            services.AddTransient<IContactEmailValidation, ContactEmailValidation>();
        }

    }
}
