using System;
using System.Web;
using AngleSharp.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Roulette.Controllers
{
    public class SteamLoginController : Controller
    {
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
        
        [HttpGet("~/signin-steam")]
        public IActionResult AfterLoginRedirection()
        {
            string steamId = HttpUtility
                .ParseQueryString(Request.QueryString.ToString())
                .Get("openid.identity")
                .Replace("https://steamcommunity.com/openid/id/", "");

            var cookieOptions = new CookieOptions {Expires = new DateTimeOffset(DateTime.Now.AddDays(1))};

            HttpContext.Response.Cookies.Append("SteamId", steamId, cookieOptions);

            return RedirectPermanent("/");
        }

        [HttpPost("~/steamLogout")]
        public IActionResult SignOut()
        {
            HttpContext.Response.Cookies.Append("SteamId", "");
            return RedirectPermanent("/");
            //return SignOut(new AuthenticationProperties { RedirectUri = "/" }, CookieAuthenticationDefaults.AuthenticationScheme);
        }
       
    }
}