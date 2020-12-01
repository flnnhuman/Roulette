using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Roulette.Context;
using Roulette.Models;

namespace Roulette_Server
{
    public class NotificationHub : Hub
    {
        private readonly AppDbContext AppDbContext;


        public NotificationHub(AppDbContext context)
        {
            AppDbContext = context;
        }

        public static async Task SendRoll(IHubContext<NotificationHub> context, string rollValue)
        {
            var spinTime = "4";


            await context.Clients.All.SendAsync("roll", string.Join('/', rollValue, spinTime));
        }

        public static async Task SendTimer(IHubContext<NotificationHub> context)
        {
            await context.Clients.All.SendAsync("timer", "20");
        }

        public static async Task SendRollHistory(IHubContext<NotificationHub> context, string history)
        {
            await context.Clients.All.SendAsync("rollHistory", history);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await Clients.Caller.SendAsync("rollHistory", string.Join(',', Program.RollsHistory));
        }

        [HubMethodName("placebet")]
        public async Task PlaceBet(string args)
        {
            var argsList = args.Split(',').ToList();
            var bet = Bet.Deserialize(args);
            Program.Bets.Add(bet);

            var user = await AppDbContext.SteamUsers.Where(x => x.SteamID == bet.SteamID).FirstOrDefaultAsync();
            user.Balance -= bet.amount;
            Console.WriteLine("got bet from {0} on {1} amount {2}", argsList[0], argsList[1], argsList[2]);
        }
    }
}