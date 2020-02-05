using ARI.EVOS.Dealer.AppServices.Extension;
using ARI.EVOS.Dealer.Command.Mapper;
using ARI.EVOS.Dealer.Infrastructure.Mappings;
using ARI.EVOS.Dealer.Query.Extension;
using ARI.EVOS.Infra;
using ARI.EVOS.Infra.Interface;
using ARI.EVOS.Infra.Service;
using AutoMapper;
using Chassis.Command.Interfaces;
using Chassis.Dapper.Extension;
using Chassis.RegisterServices.Extension;
using Chassis.Repository.NHibernate;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ARI.EVOS.CompApp.ExtensionMethod
{
    /// <summary>
    /// Extension method to extend container extension functionality
    /// </summary>
    public static class AriServicesExtension
    {
        /// <summary>
        /// Extension method for container extension functionality of app.xaml
        /// </summary>
        /// <param name="services"></param>
        /// <param name="optionAction"></param>
        public static void AddExtensionServices(this IServiceCollection services, Action<AppSettings> optionAction)
        {
            AppSettings appSettings = new AppSettings();
            optionAction.Invoke(appSettings);
            services.AddSingleton(appSettings);

            // Query (Dapper)            
            services.AddDapper(x =>
            {
                x.Value = appSettings.DBConnectionString;
            },
            typeof(QueryServicesExtention).Assembly);

            // Command (NHibernate)
            services.AddNHibernate(x =>
            {
                x.Database(OracleClientConfiguration.Oracle10.ConnectionString(appSettings.DBConnectionString)
                 .Driver<NHibernate.Driver.OracleManagedDataClientDriver>)
                 .Mappings(m => m.FluentMappings
                   .AddFromAssembly(typeof(DealerMapping).Assembly)
                 //.Conventions.Add(ForeignKey.EndsWith("Id"))
                 );
            }, ServiceLifetime.Singleton);

            //Register service
            services.AddRegisterService(typeof(ICommand<>).Assembly,
                                        typeof(AddDealerMapping).Assembly,
                                        typeof(QueryServicesExtention).Assembly);

            services.AddAutoMapper(typeof(AddDealerMapping).Assembly);

            // AppServices
            services.AddDealerServices();

            services.AddTransient<IMessage, Message>();
        }
    }
}
