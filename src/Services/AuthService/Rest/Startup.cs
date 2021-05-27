using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Kwetter.Services.AuthService.Persistence.Contexts;
using Kwetter.Services.AuthService.Application.Common.Interfaces;
using Persistence;
using Kwetter.Services.AuthService.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using KwetterShared;

namespace Kwetter.Services.AuthService.Rest
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
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
                {
                    options.LoginPath = "/api/Auth/google-login";
                })
                //TODO: Get this data from config file instead
                .AddGoogle(options => {
                    options.ClientId = Configuration.GetValue<string>("Google:ClientId");
                    options.ClientSecret = Configuration.GetValue<string>("Google:ClientSecret"); ;
                });

            services.AddControllers();
            services.AddHttpClient<IAuthService, Application.Services.AuthService>();
            services.AddPersistence(Configuration);
            services.AddInfrastructure(Configuration);
            services.AddMessaging("AuthService");
            services.AddScoped<IAuthService, Application.Services.AuthService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kwetter API", Version = "v1", Description = "The Kweet-API for the Kwetter Project" });
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rest v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}
