using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Kwetter.Services.AuthService.Application.Common.Interfaces;
using Kwetter.Services.AuthService.Application.Common.Interfaces.Services;
using Kwetter.Services.AuthService.Infrastructure;
using Kwetter.Services.AuthService.Persistence.Contexts;
using Kwetter.Services.Shared;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Persistence;
using System.Linq;

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
            var firebaseSettings = Configuration.GetSection("FirebaseConfig").GetChildren();
            var configurationSections = firebaseSettings.ToList();
            var json = JsonConvert.SerializeObject(configurationSections.AsEnumerable()
                .ToDictionary(k => k.Key, v => v.Value));
            var firebaseApp = FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromJson(json)
            });
            services.AddSingleton(firebaseApp);

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
            services.AddScoped<IAuthoService, Application.Services.AuthorizationService>();
            services.AddScoped<ITokenChecker, FirebaseVerifier>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kwetter API", Version = "v1", Description = "The Auth-API for the Kwetter Project" });
            });
            //JWTSettings.SecretKey = Configuration.GetSection("JWTSettings:SecretKey").Value;
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

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AuthContext>();
                context.Database.EnsureCreated();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}
