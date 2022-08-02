using Microsoft.Extensions.DependencyInjection;
using dotnetnbpgold.nbp.client.Models.Settings;

namespace dotnetnbpgold.nbp.client.Extensions
{
    public static class DotNetNBPGoldClientDependencyInjectionExtension
    {
        public static IServiceCollection AddDotNetNBPGoldClient(
            this IServiceCollection services,
            Action<DotNetNBPGoldClientSettings> options = null) 
        {
            if (options is not null)
            {
                services.Configure(options);
            }

            services.AddHttpClient();
            services.AddTransient<IDotNetNBPGoldClient, DotNetNBPGoldClient>();
            return services;
        }
    }
}