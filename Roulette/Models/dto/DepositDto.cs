using System.Collections.Generic;

namespace Roulette.Models.dto
{
	public class DepositDto
	{
		public List<DepositItem> items { get; set; }
		public string method { get; set; }
		public string tradeLink { get; set; }
	}
	
	public class DepositItem
	{
		public ulong items { get; set; }
		public double price { get; set; }
	}
}