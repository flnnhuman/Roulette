using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Roulette_Server
{
    public class NotificationHub : Hub
    {
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
    }
}