using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Roulette.Context;
using Roulette.Models;

namespace Roulette.Controllers
{
    public class SteamUsersController : Controller
    {
        private readonly AppDbContext AppDbContext;

        public SteamUsersController(AppDbContext context)
        {
            AppDbContext = context;
        }


        [HttpGet("~/loginView")]
        public IActionResult LoginView()
        {
            return View("Index");
        }

        [HttpGet("~/login")]
        public IActionResult Login()
        {
            return Challenge(new AuthenticationProperties {RedirectUri = "/"}, "Steam");
        }


        [HttpPost("~/steamLogout")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectPermanent("/");
            //return SignOut(new AuthenticationProperties { RedirectUri = "/" }, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<bool> IsRegisteredAsync(string steamId)
        {
            var isRegistered = await AppDbContext.SteamUsers.FirstOrDefaultAsync(x => x.SteamID == steamId) != null;
            return isRegistered;
        }

        public async Task RegisterUserAsync(string steamId)
        {
            var user = AppDbContext.SteamUsers.FirstOrDefault(x => x.SteamID == steamId);

            if (user == null)
            {
                await AppDbContext.SteamUsers.AddAsync(new SteamUsersModel
                {
                    SteamID = steamId, Balance = 0
                });
                await AppDbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateUserAsync(Player player)
        {
            var user = AppDbContext.SteamUsers.FirstOrDefault(x => x.SteamID == player.steamid);
            if (user != null)
            {
                user.avatar = player.avatar;
                user.avatarfull = player.avatarfull;
                user.avatarmedium = player.avatarmedium;
                user.communityvisibilitystate = player.communityvisibilitystate;
                user.personaname = player.personaname;
                user.personastate = player.personastate;
                user.profileurl = player.profileurl;
                user.avatarhash = player.avatarhash;
                AppDbContext.SteamUsers.Update(user);
                await AppDbContext.SaveChangesAsync();
            }
        }
    }
}