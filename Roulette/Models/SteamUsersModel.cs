using System.ComponentModel.DataAnnotations;

namespace Roulette.Models
{
    public class SteamUsersModel
    {
        [Key] public string SteamID { get; set; }
        public double Balance { get; set; }
        public int communityvisibilitystate { get; set; }
        public int profilestate { get; set; }
        public int personastate { get; set; }
        public int personastateflags { get; set; }
        public string personaname { get; set; }
        public string profileurl { get; set; }
        public string avatar { get; set; }
        public string avatarhash { get; set; }
        public string avatarmedium { get; set; }
        public string avatarfull { get; set; }
        public string TradeLink { get; set; }
        public float TotalWon { get; set; }
        public float TotalDeposited { get; set; }
    }
}