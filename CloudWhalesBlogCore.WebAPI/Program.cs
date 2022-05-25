using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace CloudWhalesBlogCore.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            /*var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
            host.Run();*/
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel();
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                    webBuilder.UseIISIntegration();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
