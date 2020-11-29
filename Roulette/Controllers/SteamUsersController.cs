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
        private AppDbContext AppDbContext;

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
           /*
        [HttpGet("~/signin-steam")]
        
        public async Task<IActionResult> signin_steam()
        {
            string steamId = HttpUtility
                .ParseQueryString(Request.QueryString.ToString())
                .Get("openid.identity")
                .Replace("https://steamcommunity.com/openid/id/", "");


            SteamUsersModel user = AppDbContext.SteamUsers.FirstOrDefault(x => x.SteamID == steamId);
            
            if (user == null)
            {
                await AppDbContext.SteamUsers.AddAsync(new SteamUsersModel()
                {
                    SteamID = steamId//, Balance = 0
                    
                });
                await AppDbContext.SaveChangesAsync();
            }


            await Authenticate(steamId);


            //TempData["SteamUsersModel"] = JsonConvert.SerializeObject(new SteamUsersModel {SteamID = steamId});
            //TempData.Keep("SteamUsersModel");


            return RedirectPermanent("/");
        }
         */ 

        
        [HttpPost("~/steamLogout")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectPermanent("/");
            //return SignOut(new AuthenticationProperties { RedirectUri = "/" }, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<bool> IsRegisteredAsync(string steamId)
        {
            bool isRegistered = await AppDbContext.SteamUsers.FirstOrDefaultAsync(x => x.SteamID == steamId) != null;
            return isRegistered;
        }

        public async Task RegisterUserAsync(string steamId)
        {
            SteamUsersModel user = AppDbContext.SteamUsers.FirstOrDefault(x => x.SteamID == steamId);
            
            if (user == null)
            {
                await AppDbContext.SteamUsers.AddAsync(new SteamUsersModel()
                {
                    SteamID = steamId//, Balance = 0
                    
                });
                await AppDbContext.SaveChangesAsync();
            } 
        }
    }
}