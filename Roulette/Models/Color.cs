using System.Collections.Generic;

namespace Roulette.Models
{
	public enum Color
	{
		Red = 1,
		Green = 2,
		Black = 3
	}

	public static class BetColors
	{
		public static readonly List<int> Red = new List<int> {1, 2, 3, 4, 5, 6, 7};
		public static readonly List<int> Black = new List<int> {8, 9, 10, 11, 12, 13, 14};
	}
}