using IdentityServer4_Base.Models;
using IdentityServer4_Base.Repositories;
using IdentityServer4_Base.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace IdentityServer4_Base
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
            services.AddScoped<ICustomUserRepository,CustomUserRepository>();
            services.AddDbContext<CustomDbContext>(options=> {
                options.UseSqlServer(Configuration.GetConnectionString("CustomConnectionString"));
            });

            var assemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = c => c.UseSqlServer(Configuration.GetConnectionString("CustomConnectionString"), sqlOptions => sqlOptions.MigrationsAssembly(assemblyName));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = c => c.UseSqlServer(Configuration.GetConnectionString("CustomConnectionString"), sqlOptions => sqlOptions.MigrationsAssembly(assemblyName));
                })
                //.AddInMemoryApiResources(Config.GetApiResources())
                //.AddInMemoryApiScopes(Config.GetApiScopes())
                //.AddInMemoryClients(Config.GetClients())
                //.AddInMemoryIdentityResources(Config.GetIdentityResources())
                //.AddTestUsers(Config.GetUsers().ToList())
                .AddDeveloperSigningCredential()
                .AddProfileService<CustomProfileService>();
            
            services.AddControllersWithViews();
        }

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
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
