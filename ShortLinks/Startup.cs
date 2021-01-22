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
using ShortLinks.Contracts;
using ShortLinks.LoggerService;
using NLog;
using System;
using System.IO;

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
            var _appSettings = Configuration.GetSection("AuthOptions") as AuthOptions;
            services.AddAutoMapper(typeof(Startup));
            services.AddOptions();
            services.AddDbContext<LinkContext>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddScoped<ILinkService, LinkService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserManagerService, UserManagerService>();
            services.ConfigureLoggerService(); //аналог через метод расширения класса ServiceExtensions services.AddScoped< ILoggerManager, LoggerManager >();
            services.AddHttpContextAccessor();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = _appSettings.ISSUER,                      
                            ValidateAudience = true,                         
                            ValidAudience = _appSettings.AUDIENCE,
                            ValidateLifetime = true,
                            IssuerSigningKey = _appSettings.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });
            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "eShopOnContainers - Catalog HTTP API",
                    Version = "v1",
                    Description = "The Catalog Microservice HTTP API. This is a Data-Driven/CRUD microservice sample"
                });
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
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}