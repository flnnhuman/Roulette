using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Roulette.Context;
using Roulette.Models;

namespace Roulette_Server
{
    public class Program
    {
        public static Queue<int> RollsHistory = new Queue<int>();
        public static List<Bet> Bets = new List<Bet>();

        public static IHubContext<NotificationHub> hubContext;
        public static AppDbContext AppDbContext;
        private static Timer timer;
        private static IHost host;


        public static void Main(string[] args)
        {
            host = CreateHostBuilder(args).Build();
            hubContext = (IHubContext<NotificationHub>) host.Services.GetService(typeof(IHubContext<NotificationHub>));
            CreateTimer();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel(opt =>
                    {
                        AppDbContext = opt.ApplicationServices.CreateScope().ServiceProvider
                            .GetService<AppDbContext>();
                    });
                });
        }


        public static void CreateTimer()
        {
            timer = new Timer(
                async e => { await Play(); },
                null,
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(27)
            );
        }

        private static async Task Play()
        {
            await NotificationHub.SendTimer(hubContext);
            await Task.Delay(TimeSpan.FromSeconds(20));

            var random = new Random();
            var rollValue = random.Next() % 15;
            Console.WriteLine(DateTime.Now + " Rolled " + rollValue);

            await NotificationHub.SendRollAsync(hubContext, rollValue.ToString());
            await Task.Delay(TimeSpan.FromSeconds(4));


            List<Bet> wonBets;
            if (rollValue == 0)
                wonBets = Bets.Where(bet => bet.Color == Color.Green).ToList();

            else if (BetColors.Black.Any(x => x == rollValue))
                wonBets = Bets.Where(bet => bet.Color == Color.Black).ToList();
            else
                wonBets = Bets.Where(bet => bet.Color == Color.Red).ToList();

            Bets.Clear();

            var uniquePlayers = wonBets.Select(o => o.SteamID).Distinct();

            AppDbContext = Roulette.Program.GetNewContext();
            foreach (var player in uniquePlayers)
            {
                var betsOfOnePlayer = wonBets.Where(bet => bet.SteamID == player);
                var userModel = await AppDbContext.SteamUsers.Where(model => model.SteamID == player)
                    .FirstOrDefaultAsync();
                foreach (var bet in betsOfOnePlayer)
                {
                    var won = rollValue == 0 ? bet.amount * 14 : bet.amount * 2;
                    userModel.Balance += won;
                    userModel.TotalWon += won;
                }

                AppDbContext.SteamUsers.Update(userModel);
            }

            await AppDbContext.SaveChangesAsync();

            if (RollsHistory.Count >= 10) RollsHistory.Dequeue();
            RollsHistory.Enqueue(rollValue);

            await NotificationHub.SendAllBetsAsync(hubContext);
            await NotificationHub.SendRollHistoryAsync(hubContext, string.Join(',', RollsHistory));
        }
    }
}