using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static Queue<int> RollsHistory = new();
        public static int LastRoll;
        public static List<BetModel> Bets = new();

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
                    webBuilder.UseUrls("http://localhost:5002/");
                    webBuilder.UseKestrel(opt =>
                    {
                        AppDbContext = opt.ApplicationServices.CreateScope().ServiceProvider
                            .GetService<AppDbContext>();
                        RollsHistory = new Queue<int>(AppDbContext.GamesHistory
                            .Skip(Math.Max(0, AppDbContext.GamesHistory.Count() - 10))
                            .Select(model => model.WonNumber));
                        if (RollsHistory.Count == 0)
                        {
                            LastRoll = 0;
                        }
                        else
                        {
                            LastRoll = RollsHistory.Last();
                        }
                    });
                });
        }


        private static async Task CreateTimer()
        {
            var timeInfo = TimeInfo.GetSomeTimeInfo();
            
            timer = new Timer(
                async e => { await Play(); },
                null,
                TimeSpan.FromSeconds(30).Subtract(timeInfo.TimeSpanFromTheStartOfTheRound),
                TimeSpan.FromSeconds(30)
            );
        }

        private static async Task Play()
        {
            var timeInfo = TimeInfo.GetSomeTimeInfo();
            await NotificationHub.SendTimerAsync(hubContext);
            await Task.Delay(TimeSpan.FromSeconds(25));

            var random = new Random();
            LastRoll = random.Next() % 15;
            Console.WriteLine(DateTime.Now + " Rolled " + LastRoll);

            await NotificationHub.SendRollAsync(hubContext, LastRoll.ToString());
            await Task.Delay(TimeSpan.FromSeconds(4));

            Color wonColor;
            List<BetModel> wonBets;
            if (LastRoll == 0)
            {
                wonBets = Bets.Where(bet => bet.Color == Color.Green).ToList();
                wonColor = Color.Green;
            }

            else if (BetColors.Black.Any(x => x == LastRoll))
            {
                wonBets = Bets.Where(bet => bet.Color == Color.Black).ToList();
                wonColor = Color.Black;
            }
            else
            {
                wonBets = Bets.Where(bet => bet.Color == Color.Red).ToList();
                wonColor = Color.Red;
            }


            var uniquePlayers = wonBets.Select(o => o.SteamID).Distinct();

            AppDbContext = Roulette.Program.GetNewContext();
            foreach (var player in uniquePlayers)
            {
                var betsOfOnePlayer = wonBets.Where(bet => bet.SteamID == player);
                var userModel = await AppDbContext.SteamUsers.Where(model => model.SteamID == player)
                    .FirstOrDefaultAsync();
                foreach (var bet in betsOfOnePlayer)
                {
                    var won = LastRoll == 0 ? bet.Amount * 14 : bet.Amount * 2;
                    userModel.Balance += won;
                    userModel.TotalWon += won;
                }

                AppDbContext.SteamUsers.Update(userModel);
            }

            var currentGame = new GameModel
                {Timestamp = DateTime.UtcNow, WonNumber = LastRoll, WonColor = wonColor, AllBets = Bets};
            await AppDbContext.GamesHistory.AddAsync(currentGame);
            await AppDbContext.SaveChangesAsync();
            Bets.Clear();
            if (RollsHistory.Count >= 10) RollsHistory.Dequeue();
            RollsHistory.Enqueue(LastRoll);

            await NotificationHub.SendAllBetsAsync(hubContext);
            await NotificationHub.SendRollHistoryAsync(hubContext, string.Join(',', RollsHistory));
        }
    }
}