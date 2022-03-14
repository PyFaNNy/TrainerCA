using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Trainer.API.Infrastructure.Filters;
using Trainer.Application;
using Trainer.Chart;
using Trainer.CSVParserService;
using Trainer.EmailService;
using Trainer.Persistence;
using OpenIddict.Validation.AspNetCore;

namespace Trainer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.Filters.Add(new ApiExceptionFilterAttribute()))
                .AddFluentValidation()
                .AddDataAnnotationsLocalization()
                .AddViewLocalization();

            services.AddApplication();
            services.AddPersistence(Configuration);
            services.AddEmailService(Configuration);
            services.AddCSVParserService(Configuration);
            services.AddSignalR();
            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru")
                };

                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
            services.AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore()
                        .UseDbContext<TrainerDbContext>()
                        .ReplaceDefaultEntities<Guid>();
                })
                .AddValidation(options =>
                {
                    options.SetIssuer(Configuration["Authority"]);
                    options.AddEncryptionKey(
                        new SymmetricSecurityKey(Convert.FromBase64String(Configuration["JWTEncryptionKey"])));
                    options.UseSystemNetHttp();
                    options.UseAspNetCore();
                });
            services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.Use(async (context, next) =>
            {
                var bearer = context.Request.Cookies["access_token"];
                context.Request.Headers.Add("Authorization", "Bearer " + bearer);
                await next().ConfigureAwait(false);
            });

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRequestLocalization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
                endpoints.MapHub<ChartHub>("/chart");
            });
        }
    }
}
