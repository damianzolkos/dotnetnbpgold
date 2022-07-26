using Microsoft.Extensions.DependencyInjection;
using dotnetnbpgold.nbp.client.Settings;

namespace dotnetnbpgold.nbp.client.Extensions
{
    public static class DotNetNBPGoldClientDependencyInjectionExtension
    {
        public static IServiceCollection AddDotNetNBPGoldClient(
            this IServiceCollection services,
            Action<DotNetNBPGoldClientSettings> options = null) 
        {
           services.Configure(options);
           services.AddTransient<IDotNetNBPGoldClient, DotNetNBPGoldClient>();
           return services;
        }
    }
}