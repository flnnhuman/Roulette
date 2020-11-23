using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Roulette_Server
{
    public class NotificationHub : Hub
    {
        public static async Task SendRoll(IHubContext<NotificationHub> context)
        {
            Random random = new Random();
            var rollValue = (random.Next() % 15).ToString();
            var spinTime = "4";


            await context.Clients.All.SendAsync("roll", string.Join('/', rollValue, spinTime));
        }

        public static async Task SendTimer(IHubContext<NotificationHub> context)
        {
            await context.Clients.All.SendAsync("timer", "20");
        }
    }
}