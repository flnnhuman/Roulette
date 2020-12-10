using Microsoft.AspNetCore.Mvc;
using Roulette.Context;

namespace Roulette.Controllers
{
    public class DBController : ControllerBase
    {
        private readonly AppDbContext AppDbContext;

        public DBController(AppDbContext context)
        {
            AppDbContext = context;
        }
    }
}