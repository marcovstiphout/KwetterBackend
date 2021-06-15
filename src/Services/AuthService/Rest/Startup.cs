using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Kwetter.Services.AuthService.Persistence.Contexts;
using Kwetter.Services.AuthService.Application.Common.Interfaces;
using Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using KwetterShared;
using Kwetter.Services.AuthService.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
                .AddGoogle(options => {
                    options.ClientId = Configuration.GetValue<string>("Google:ClientId");
                    options.ClientSecret = Configuration.GetValue<string>("Google:ClientSecret"); ;
                });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://securetoken.google.com/kwetter-cf7f5";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/kwetter-cf7f5",
                        ValidateAudience = true,
                        ValidAudience = "kwetter-cf7f5",
                        ValidateLifetime = true
                    };
                });

            services.AddControllers();
            services.AddHttpClient<IAuthService, Application.Services.AuthService>();
            services.AddPersistence(Configuration);
            services.AddMessaging("AuthService");
            services.AddScoped<IAuthService, Application.Services.AuthService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kwetter API", Version = "v1", Description = "The Auth-API for the Kwetter Project" });
            });
            JWTSettings.SecretKey = Configuration.GetSection("JWTSettings:SecretKey").Value;
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

            //app.UseHttpsRedirection();

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
