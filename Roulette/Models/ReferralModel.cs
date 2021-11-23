
using System.ComponentModel.DataAnnotations;

namespace Roulette.Models
{
	public class ReferralModel
	{
		[Key] public uint Id { get; set; }
		public string Name { get; set; }
		public double Amount { get; set; }
		public uint Usages { get; set; }
		
		public string SteamUsersId { get; set; }
		public SteamUsersModel SteamUsers { get; set; }
	}
}