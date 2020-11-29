using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Roulette.Context;
using Roulette.Models;

namespace Roulette.Controllers
{
    public class HomeController : Controller
    {
        HomeModel HomeModel;


        private AppDbContext AppDbContext;

        public HomeController(AppDbContext context, HomeModel homeModel)
        {
            AppDbContext = context;
            HomeModel = homeModel;
        }

        public IActionResult RedirectToLogin()
        {
            return RedirectPermanent("/Login");
        }

        
        public async Task<IActionResult> Index()
        {
            var abc = User.Claims.Where(c => c.Type == "steamID").Select(c => c.Value).SingleOrDefault();
            SteamUsersModel user =
                await AppDbContext.SteamUsers.FirstOrDefaultAsync(x => x.SteamID ==abc);
            HomeModel.User = user;
            return View(HomeModel);
        }
    }
}