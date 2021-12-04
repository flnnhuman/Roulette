using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Roulette.Context;
using Roulette.Models;

namespace Roulette.Controllers
{
	public class HistoryController : Controller
	{
		private readonly AppDbContext AppDbContext;
		public HistoryController(AppDbContext context)
		{
			AppDbContext = context;
		}

		public List<GameModel> GetLastGames(int page, int size)
		{
			if (page < 0)
			{
				return new List<GameModel>();
			}
			return AppDbContext.GamesHistory.AsEnumerable().Skip(20 * page).Take(20).ToList();
		}
		public GameModel GetGame(long id)
		{
			return AppDbContext.GamesHistory.Find(id);
		}
	}
}