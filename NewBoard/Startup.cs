﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NewBoardRestApi;
using NewBoardRestApi.ArticleApi;
using NewBoardRestApi.DataModel;
using NewBoardRestApi.FeedApi;
using NewBoardRestApi.GroupApi;
using NewBoardRestApi.PermissionApi;
using NewBoardRestApi.TagApi;
using NewBoardRestApi.UserApi;
using ServerSideSpaTools;
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


        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddMvc();

            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationExpanders.Add(new LocationExpander());
            });

            var session = services.AddSession(options =>
            {
                options.Cookie.Name = ".NewsBoard.Session";
                options.IdleTimeout = TimeSpan.MaxValue;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;


            });
            var sp = services.BuildServiceProvider();
            var iHttpContextAccessor = sp.GetService<IHttpContextAccessor>();
            services.AddScoped((i) => new SessionObject(iHttpContextAccessor.HttpContext.Session));


            services.AddDbContext<NewsBoardContext>(o => o.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            

            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            });


            services.AddScoped<ArticleApi>();
            services.AddScoped<FeedApi>();
            services.AddScoped<GroupApi>();
            services.AddScoped<PermissionApi>();
            services.AddScoped<TagApi>();
            services.AddScoped<UserApi>();
            services.AddScoped<AuthenticationApi>();



            // services.AddCookieAuthentication();

            services.AddResponseCaching();
            services.AddAuthentication();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                // add the new route here.
                routes.MapRoute(name: "areaRoute", template: "{area:exists}/{controller}/{action}", defaults: new { controller = "Home", action = "Index" });
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseResponseCaching();
        }
    }
}
