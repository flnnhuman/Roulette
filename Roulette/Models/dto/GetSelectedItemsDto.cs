using System.Collections.Generic;
using Newtonsoft.Json;

namespace Roulette.Models.dto
{
	public class GetSelectedItemsDto
	{
		[JsonProperty("inv")]
		public List<string> Items { get; set; }
		[JsonProperty("invName")]
		public string InvName { get; set; }
	}
}