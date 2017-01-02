using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using NewBoardRestApi.Http;
using System.IO;

namespace NewBoardRestApi
{
    public class Program
    {
        public static void plop(string url)
        {
            var result = new HttpClientWrapper(url)
                .GetResponse()
                .ToXDocument()
                .ToFeedClientStrategy(url)
                .FeedClient()
                .Items();
        }

        public static void Main(string[] args)
        {
            plop("http://www.yegor256.com/rss.xml");

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
