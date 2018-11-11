using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ClassBoots.Models;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace ClassBoots
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
   

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddRazorPagesOptions(options =>
            {
                //options.Conventions.AuthorizePage("/About");
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            services.AddDbContext<ModelContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ModelContext")));

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = "823435120787-l04052src5miam0j01bo6vm3j07nubkc.apps.googleusercontent.com"; // Configuration["google:client_id"];
                googleOptions.ClientSecret = "N17rMa2M8cL112rh8mkCRNhI"; // Configuration["google:client_secret"];
            }).AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = "313214946078073"; // Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = "91631cfa75a7fcb0ef882d18ad92efa6"; // Configuration["Authentication:Facebook:AppSecret"];
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
