using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Roulette_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        public NotificationsController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] string title)
        {
            await _hubContext.Clients.All.SendAsync("roll", title);
            Console.WriteLine("Notification has been sent successfully!");
            return Ok("Notification has been sent successfully!");
        }
    }
}