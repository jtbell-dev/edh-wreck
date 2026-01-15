using EdhWreck.Biz.Abstractions;

namespace EdhWreck.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddTransient<IScryfallApiService, ScryfallApiService>();
            return services;
        }
    }
}
