namespace Trainer.OpenIdDict
{
    using global::OpenIddict.Validation.AspNetCore;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;
    using Persistence;
    using Settings;
    using static global::OpenIddict.Abstractions.OpenIddictConstants;

    public class Startup
    {
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.Env = env;
        }

        private IConfiguration Configuration
        {
            get;
        }

        private IWebHostEnvironment Env
        {
            get;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllersWithViews();

            services.AddPersistence(this.Configuration);

            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = Claims.Role;
                options.ClaimsIdentity.EmailClaimType = Claims.Email;
            });

            services.AddOpenIddict()
                 .AddCore(options =>
                 {
                     options.UseEntityFrameworkCore()
                         .UseDbContext<TrainerDbContext>()
                         .ReplaceDefaultEntities<Guid>();
                 })
                .AddServer(options =>
                {
                    options.SetTokenEndpointUris("/connect/token")
                        .SetLogoutEndpointUris("/connect/logout");

                    options.AllowPasswordFlow()
                        .AllowRefreshTokenFlow();

                    if (this.Env.IsDevelopment())
                    {
                        options
                            .AddDevelopmentSigningCertificate();
                    }
                    else
                    {
                        options
                        .AddSigningCertificate(this.Configuration["SigningCertificateThumbprint"]);
                    }


                    options.AddEncryptionKey(new SymmetricSecurityKey(
                        Convert.FromBase64String(this.Configuration["JWTEncryptionKey"])));

                    options.SetAccessTokenLifetime(TimeSpan.FromMinutes(5));

                    options.UseAspNetCore()
                        .EnableTokenEndpointPassthrough()
                        .EnableLogoutEndpointPassthrough();
                })
                .AddValidation(options =>
                {
                    options.UseLocalServer();
                    options.UseAspNetCore();
                });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
            });
            services.AddCors();
            services.AddHostedService<DefaultData>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors(x => x.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(options =>
            {
                options.MapControllers();
                options.MapDefaultControllerRoute();
            });
            app.UseWelcomePage();
        }
    }
}
