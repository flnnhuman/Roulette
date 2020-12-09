using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Roulette.Context;
using Roulette.Controllers;

namespace Roulette
{
    public class Program
    {
        public static Configuration configuration;

        public static readonly string ConnectionString =
            "server=localhost;user=root;password=qwer1234;database=roulette;";

        public static void Main(string[] args)
        {
            DepositController.GetPriceList();
            configuration = Configuration.Load("config.json").Result;
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }

        public static AppDbContext GetNewContext()
        {
            var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(ConnectionString, new MySqlServerVersion(new Version(8, 0, 22)))
                .Options;
            return new AppDbContext(contextOptions);
        }
    }
}