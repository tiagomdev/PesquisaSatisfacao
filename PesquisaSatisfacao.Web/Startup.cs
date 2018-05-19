using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using PesquisaSatisfacao.Core.Domain.Users;
using PesquisaSatisfacao.Core.Application;
using PesquisaSatisfacao.Core.Infrastructure.Database.Repository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PesquisaSatisfacao.Web.Models.Users;
using PesquisaSatisfacao.Core.Domain.Surveys;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace PesquisaSatisfacao.Web
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
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IUserStore<ApplicationUser>, ExampleUserStore>();
            services.AddSingleton<IRoleStore<IdentityRole>, ExampleRoleStore>();

            services.Configure<IdentityOptions>(options =>
            {
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                // If the LoginPath isn't set, ASP.NET Core defaults 
                // the path to /Account/Login.
                options.LoginPath = "/User/Login";
                // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                // the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/User/Login";
                options.SlidingExpiration = true;
            });

            var cultures = new List<CultureInfo> { new CultureInfo("pt-br") };

            services.Configure<RequestLocalizationOptions>(
                options =>
                {

                    options.DefaultRequestCulture = new RequestCulture(culture: "pt-br", uiCulture: "pt-br");
                    options.SupportedCultures = cultures;
                    options.SupportedUICultures = cultures;
                });

            services.AddScoped<UserAppService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<SurveyAppService>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
