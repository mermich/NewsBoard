using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Http;
using System;

namespace NewsBoard
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }


        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddMvc();

            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationExpanders.Add(new MySharedLocationRemapper());
            });

            var session = services.AddSession(options =>
            {
                options.CookieName = ".NewsBoard.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });

            services.AddAuthentication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }



            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "NewsBoardScheme",
                LoginPath = new PathString("/User/UserLogin/"),
                AccessDeniedPath = new PathString("/User/Denied/"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                SlidingExpiration = true
            });

            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                // add the new route here.
                routes.MapRoute(name: "areaRoute", template: "{area:exists}/{controller}/{action}", defaults: new { controller = "Home", action = "Index" });
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class MySharedLocationRemapper : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            //return viewLocations.Select(f => f.Replace("/Views/", "/wwwroot/"));

            // Swap /Shared/ for /_Shared/
            var copy = viewLocations.ToList();
            copy.Add("~/wwwroot/{2}/{1}/{0}.cshtml");
            copy.Add("~/wwwroot/{1}/{0}.cshtml");
            copy.Add("~/wwwroot/{0}.cshtml");
            return copy;

        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            // do nothing.. not entirely needed for this 
            //context.ViewName = context.ViewName.Replace("EditorTemplates/", "");
        }
    }
}
