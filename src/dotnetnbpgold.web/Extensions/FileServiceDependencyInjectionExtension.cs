using dotnetnbpgold.web.Models.Settings;
using dotnetnbpgold.web.Services;

namespace dotnetnbpgold.web.Extensions
{
    public static class FileServiceDependencyInjectionExtension
    {
        public static IServiceCollection AddFileService(
            this IServiceCollection services,
            Action<FileServiceSettings> options = null) 
        {
            if (options is not null) {
                services.Configure(options);
            }
            services.AddTransient<IFileService, FileService>();
            return services;
        }
    }
}