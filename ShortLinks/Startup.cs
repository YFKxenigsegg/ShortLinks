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
using ShortLinks.Services;
using ShortLinks.LoggerService;
using NLog;
using System;
using System.IO;
using System.Text;
using ShortLinks.Infrastructure.Middleware;
using Serilog;

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
            services.AddSwagger();
            services.AddHttpContextAccessor();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = true;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = appSettings.ISSUER,                      
                            ValidateAudience = true,                         
                            ValidAudience = appSettings.AUDIENCE,
                            ValidateLifetime = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.KEY)),
                            ValidateIssuerSigningKey = true,
                        };
                    });
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerDocumentation();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.ConfigureCustomExceptionMiddleware();
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