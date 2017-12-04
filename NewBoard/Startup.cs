using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServerSideSpaTools;
using System;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using NewBoardRestApi.TagApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using NewBoardRestApi;
using Microsoft.EntityFrameworkCore;
using NewBoardRestApi.DataModel;
using NewBoardRestApi.ArticleApi;
using NewBoardRestApi.FeedApi;
using NewBoardRestApi.GroupApi;
using NewBoardRestApi.PermissionApi;
using NewBoardRestApi.UserApi;

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
            services.AddScoped((i) => new SessionObject(iHttpContextAccessor.HttpContext.Session.GetInt32("UserId").GetValueOrDefault()));


            services.AddDbContext<NewsBoardContext>(o => o.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            
         
            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            });

            services.AddScoped((i) => new ArticleApi(i.GetService< NewsBoardContext >(), i.GetService<SessionObject>()));
            services.AddScoped((i) => new FeedApi(i.GetService<NewsBoardContext>(), i.GetService<SessionObject>()));
            services.AddScoped((i) => new GroupApi(i.GetService<NewsBoardContext>(), i.GetService<SessionObject>()));
            services.AddScoped((i) => new PermissionApi(i.GetService<NewsBoardContext>(), i.GetService<SessionObject>()));
            services.AddScoped((i) => new TagApi(i.GetService<NewsBoardContext>(), i.GetService<SessionObject>()));
            services.AddScoped((i) => new UserApi(i.GetService<NewsBoardContext>(), i.GetService<SessionObject>()));
            services.AddScoped((i) => new AuthenticationApi(i.GetService<NewsBoardContext>()));
            


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
