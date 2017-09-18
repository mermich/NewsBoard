using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using NewBoardRestApi.DataModel;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace NewBoardRestApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();


            // new FeedApi.FeedApi(3).RefreshFeedsArticles();
            //new FeedApi.FeedApi(3).RefreshFeedInformations();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddScoped<NewsBoardContext>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                new Info
                {
                    Title = "Stop Web Crawlers Update API",
                    Version = "v1",
                    Description = "Stop Web Crawlers Update API to enable the update of Referer Spammer Lists",
                    TermsOfService = "None"
                    //Contact = new Contact { Name = "threenine.co.uk", Email = "support@threenine.co.uk", Url = "https://threenine.co.uk" }
                });

                c.DescribeAllEnumsAsStrings();
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "SWCapi.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();
            app.UseMvc();


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WalkFido API V1");
            });

        }
    }
}
