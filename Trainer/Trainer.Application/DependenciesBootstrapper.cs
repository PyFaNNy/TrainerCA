namespace Trainer.Application
{
    using System.Reflection;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Trainer.Application.Behaviours;
    using Trainer.Settings.Error;

    public static class DependenciesBootstrapper
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            services.Configure<BaseUserErrorSettings>(configuration.GetSection("BaseUserErrorSettings"));
            services.Configure<CSVErrorSettings>(configuration.GetSection("CSVErrorSettings"));
            services.Configure<ExaminationErrorSettings>(configuration.GetSection("ExaminationErrorSettings"));
            services.Configure<OTPCodesErrorSettings>(configuration.GetSection("OTPCodesErrorSettings"));
            services.Configure<PatientErrorSettings>(configuration.GetSection("PatientErrorSettings"));
            services.Configure<ResultsErrorSettings>(configuration.GetSection("ResultsErrorSettings"));

            return services;
        }
    }
}
