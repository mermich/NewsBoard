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

    public static class plop
    {
        public static void AddMyServices(this IServiceCollection services, IServiceCollection context)
        {
        }
    }


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

            services.AddMyServices(null);
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

            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                // add the new route here.
                routes.MapRoute(name: "areaRoute", template: "{area:exists}/{controller}/{action}", defaults: new { controller = "Home", action = "Index" });
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                //routes.MapRoute("spa-fallback", "{*anything}", new { controller = "Home", action = "Index" });
                //routes.MapWebApiRoute("defaultApi", "api/{controller}/{id?}");
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
            copy.Add("~/wwwroot/Admin/{1}/{0}.cshtml");
            copy.Add("~/wwwroot/Controls/{1}/{0}.cshtml");
            copy.Add("~/wwwroot/Feed/{1}/{0}.cshtml");
            copy.Add("~/wwwroot/User/{1}/{0}.cshtml");
            copy.Add("~/wwwroot/Article/{1}/{0}.cshtml");
            copy.Add("~/wwwroot/Tag/{1}/{0}.cshtml");
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
