namespace Trainer.CSVParserService
{
    using Application.Interfaces;
    using global::CSVParserService.Services;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependenciesBootstrapper
    {
        public static IServiceCollection AddCSVParserService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICsvParserService, CsvParserService>();

            return services;
        }
    }
}
