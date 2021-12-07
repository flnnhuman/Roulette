using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RestSharp;
using Roulette.Context;
using Roulette.Models;
using Roulette.Models.dto;

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

        public static async Task SendTimerAsync(IHubContext<NotificationHub> context)
        {
            await context.Clients.All.SendAsync("timer", "25");
        }

        public static async Task SendRollHistoryAsync(IHubContext<NotificationHub> context, string history)
        {
            await context.Clients.All.SendAsync("RollHistory", history);
        }

        public static async Task SendBetHistoryAsync(IHubContext<NotificationHub> context)
        {
            await context.Clients.All.SendAsync("BetsList", JsonConvert.SerializeObject(Program.Bets));
        }
        public static async Task SendUpdateBalanceAsync(IHubContext<NotificationHub> context, string conectionID,
            double userBalance)
        {
            await context.Clients.Client(conectionID).SendAsync("updateBalance", userBalance.ToString());
        }


        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            await Clients.Caller.SendAsync("BetsList", JsonConvert.SerializeObject(Program.Bets));
            List<MessageModel> chatHistory = AppDbContext.ChatHistory.ToList();
            await Clients.Caller.SendAsync("ChatHistory",
                JsonConvert.SerializeObject(chatHistory.Skip(Math.Max(0, chatHistory.Count - 100))));
        }

        [HubMethodName("placebet")]
        public async Task PlaceBetAsync(string args)
        {
            var bet = JsonConvert.DeserializeObject<BetModel>(args);
            Program.Bets.Add(bet);

            var user = await AppDbContext.SteamUsers.FindAsync(bet.SteamID);
            if (user != null)
            {
                if (user.Balance < bet.Amount)
                {
                    return;
                }
                user.Balance -= bet.Amount;
                AppDbContext.SteamUsers.Update(user);
                await AppDbContext.SaveChangesAsync();
                
                Console.WriteLine("got bet from {0} on {1} amount {2}", bet.SteamID, bet.Color, bet.Amount);

                await SendBetHistoryAsync(hubContext);
                await SendUpdateBalanceAsync(hubContext, Context.ConnectionId, user.Balance);
            }
        }

        [HubMethodName("chatmessage")]
        public async Task ChatMessageAsync(string args)
        {
            MessageModel message = JsonConvert.DeserializeObject<MessageModel>(args);
            await AppDbContext.ChatHistory.AddAsync(message);
            await AppDbContext.SaveChangesAsync();
            await hubContext.Clients.All.SendAsync("chatmessage", args);
        }

        [HubMethodName("GetGamesHistory")]
        public async Task GetGamesHistoryAsync()
        {
            await Clients.Caller.SendAsync("RollHistory", string.Join(',', Program.RollsHistory));
        }
        [HubMethodName("GetTimer")]
        public async Task GetTimerAsync()
        {
            var timeInfo = TimeInfo.GetSomeTimeInfo();
            var time = timeInfo.TimeSpanFromTheStartOfTheRound.TotalSeconds.ToString(CultureInfo.InvariantCulture);
            await Clients.Caller.SendAsync("GetTimer", string.Join('/', time, Program.LastRoll));
        }
        
        [HubMethodName("DepositItems")]
        public async Task DepositItemsAsync(string args)
        {
            var depositInfo = JsonConvert.DeserializeObject<DepositDto>(args);
            if (depositInfo == null)
            {
                return;
            }
            var client = new RestClient(new Uri("http://127.0.0.1:1242"));
            var request = new RestRequest("Deposit/SendTradeOffer/", Method.GET);
            request.Parameters.Add(new Parameter("method", depositInfo.method, ParameterType.QueryString));
            request.Parameters.Add(new Parameter("data", JsonConvert.SerializeObject(depositInfo.items.Select(x => x.items).ToArray()), ParameterType.QueryString));
            request.Parameters.Add(new Parameter("tradeLink", depositInfo.tradeLink, ParameterType.QueryString));
            var queryResult = client.Execute<string>(request).Data;
            if (ulong.TryParse(queryResult, out ulong tradeId))
            {
                var tradeSentDto = new TradeSentDto();
                tradeSentDto.TradeOfferId = tradeId;
                tradeSentDto.Message = string.Format("Trade offer was sent successfully! https://steamcommunity.com/tradeoffer/{0}/",
                        tradeId);
                tradeSentDto.Price = depositInfo.items.Select(x => x.price).Sum();
                await Clients.Caller.SendAsync("tradeWasSent", JsonConvert.SerializeObject(tradeSentDto));
            }
        }
    }
}