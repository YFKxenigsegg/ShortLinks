using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShortLinks.BLL.Interfaces;
using ShortLinks.BLL.Services;
using ShortLinks.DAL.EF;
using ShortLinks.Auth.Common;
using ShortLinks.DAL.Interfaces;
using ShortLinks.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using ShortLinks.Services;
using ShortLinks.LoggerService;
using NLog;
using System;
using System.IO;
using ShortLinks.Infrastructure.Middleware;

namespace ShortLinks
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var authOptionsConfiguration = Configuration.GetSection("AuthOptions");
            services.Configure<AuthOptions>(authOptionsConfiguration);
            var appSettings = new AuthOptions();
            Configuration.GetSection("AuthOptions").Bind(appSettings);
            services.AddAutoMapper(typeof(Startup));
            services.AddOptions();
            services.AddDbContext<LinkContext>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddScoped<ILinkService, LinkService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserManagerService, UserManagerService>();
            services.ConfigureLoggerService();
            services.AddHttpContextAccessor();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = appSettings.ISSUER,                      
                            ValidateAudience = true,                         
                            ValidAudience = appSettings.AUDIENCE,
                            ValidateLifetime = true,
                            IssuerSigningKey = appSettings.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });
            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger()
             .UseSwaggerUI(c =>
             {
                 c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
             });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.ConfigureCustomExceptionMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}