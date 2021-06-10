using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SelfieAWookie.Core.Infrastructure.Data;
using SelfieAWookieAPI.ExtensionMethods;

namespace SelfieAWookieAPI
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
            services.AddDbContext<SelfieContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SelfieDatabase"), sqlOptions => { })
            );
            services.AddDefaultIdentity<IdentityUser>(options => { options.SignIn.RequireConfirmedEmail = true; })
                .AddEntityFrameworkStores<SelfieContext>();
            services.AddCustomSecurity(Configuration);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "SelfieAWookieAPI", Version = "v1"});
            });
            services.AddInjections();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SelfieAWookieAPI v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(SecurityMethods.TestPolicy);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}