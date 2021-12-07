using System.Linq;
using System.Threading.Tasks;
using BlazorPagination;
using Microsoft.AspNetCore.Mvc;
using Roulette.Context;
using Roulette.Models;
using Microsoft.EntityFrameworkCore;

namespace Roulette.Controllers {
    public class HistoryController : Controller {
        private readonly AppDbContext AppDbContext;
        public HistoryController(AppDbContext context) {
            AppDbContext = context;
        }

        public async Task<PagedResult<GameModel>> GetLastGames(int page, int pageSize) {
            return await AppDbContext.GamesHistory.Include(x => x.AllBets).OrderByDescending(x => x.Timestamp).ToPagedResultAsync(page, pageSize);
        }
        public GameModel GetGame(long id) {
            return AppDbContext.GamesHistory.Find(id);
        }
    }
}