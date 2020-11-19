using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Roulette.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult RedirectToLogin()
        {
            return RedirectPermanent("/Login");
        }

        public IActionResult  Index()
        {
            return View();
        }
    }
}