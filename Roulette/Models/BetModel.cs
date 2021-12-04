using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Roulette.Models
{
    public class BetModel
    {
        [Key] public ulong Id { get; set; }
        public float Amount { get; set; }
        public Color Color { get; set; }
        public string ConnectionId { get; set; }
        public string SteamID { get; set; }
    }
}