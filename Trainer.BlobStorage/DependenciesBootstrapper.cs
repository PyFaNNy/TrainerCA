using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trainer.Application.Interfaces;
using Trainer.BlobStorage.Services;
using Trainer.Settings;
using Trainer.Settings;

namespace Prixy.BlobStorage
{
    public static class DependenciesBootstrapper
    {
        public static IServiceCollection AddBlobStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorageSettings"));

            services.AddTransient<IBlobStorageService, BlobStorageService>();

            return services;
        }
    }
}
