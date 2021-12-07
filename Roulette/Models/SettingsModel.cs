using System.ComponentModel.DataAnnotations;

namespace Roulette.Models
{
	public class SettingsModel
	{
		
		[Key] public int id { get; set; }
		public bool ChatEnable { get; set; }
		public int NextRoll { get; set; }
	}
}