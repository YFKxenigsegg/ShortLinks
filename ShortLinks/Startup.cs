using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShortLinks.DAL;
using ShortLinks.DAL.EF;

namespace ShortLinks
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LinkContext>(options =>
                   options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            using (var scope = app.ApplicationServices.CreateScope())
            {
                LinkContext context = scope.ServiceProvider.GetRequiredService<LinkContext>();
                DBInitializer.Initial(context);
            }
        }
    }
}
