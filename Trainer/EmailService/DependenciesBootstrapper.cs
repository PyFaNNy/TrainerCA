namespace Trainer.EmailService
{
    using Application.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Trainer.Settings;
    using Microsoft.Extensions.Configuration;
    using Trainer.EmailService.Services;

    public static class DependenciesBootstrapper
    {
        public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, EmailService>();

            return services;
        }
    }
}
