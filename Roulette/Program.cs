using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Roulette
{
    public class Program
    {
        public static Configuration configuration;

        public static void Main(string[] args)
        {
            configuration = Configuration.Load("config.json").Result;
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}