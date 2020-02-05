using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ARI.EVOS.Dealer.Query.Extension
{
    /// <summary>
    /// Query service extension method for add dealer
    /// </summary>
    public static class QueryServicesExtention
    {
        public static void AddQueryServices(this IServiceCollection services)
        {              
            services.AddAutoMapper(typeof(QueryServicesExtention).Assembly);            
        }
    }
}
