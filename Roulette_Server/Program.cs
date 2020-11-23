using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;


namespace Roulette_Server
{
    public class Program
    {
        public static IHubContext<NotificationHub> hubContext;
        private static Timer timer;

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            hubContext = (IHubContext<NotificationHub>) host.Services.GetService(typeof(IHubContext<NotificationHub>));
            CreateTimer();
            host.Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });


        public static void CreateTimer()
        {
            timer = new Timer(
                async e =>
                {
                    await NotificationHub.SendTimer(hubContext);
                    await Task.Delay(TimeSpan.FromSeconds(20));
                    
                    await NotificationHub.SendRoll(hubContext);
                    await Task.Delay(TimeSpan.FromSeconds(4));
                    Console.WriteLine("Roll was sent");
                },
                null,
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(27)
            );
        }
    }
}