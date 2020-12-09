using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Roulette.Context;
using Roulette.Models;

namespace Roulette_Server
{
    public class NotificationHub : Hub
    {
        private readonly AppDbContext AppDbContext;
        private readonly IHubContext<NotificationHub> hubContext;

        public NotificationHub(AppDbContext context, IHubContext<NotificationHub> hubContext)
        {
            AppDbContext = context;
            this.hubContext = hubContext;
        }

        public static async Task SendRollAsync(IHubContext<NotificationHub> context, string rollValue)
        {
            var spinTime = "4";


            await context.Clients.All.SendAsync("roll", string.Join('/', rollValue, spinTime));
        }

        public static async Task SendTimer(IHubContext<NotificationHub> context)
        {
            await context.Clients.All.SendAsync("timer", "20");
        }

        public static async Task SendRollHistoryAsync(IHubContext<NotificationHub> context, string history)
        {
            await context.Clients.All.SendAsync("rollHistory", history);
        }

        public static async Task SendAllBetsAsync(IHubContext<NotificationHub> context)
        {
            await context.Clients.All.SendAsync("BetsList", JsonConvert.SerializeObject(Program.Bets));
        }


        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await Clients.Caller.SendAsync("rollHistory", string.Join(',', Program.RollsHistory));
            await Clients.Caller.SendAsync("BetsList", JsonConvert.SerializeObject(Program.Bets));
        }

        [HubMethodName("placebet")]
        public async Task PlaceBet(string args)
        {
            var argsList = args.Split(',').ToList();
            var bet = Bet.Deserialize(args);
            Program.Bets.Add(bet);


            Console.WriteLine("got bet from {0} on {1} amount {2}", argsList[0], argsList[1], argsList[2]);

            await SendAllBetsAsync(hubContext);
        }
    }
}