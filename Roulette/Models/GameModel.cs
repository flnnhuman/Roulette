using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Roulette.Models
{
    public class GameModel
    {
        [Key] public ulong Id { get; set; }
        public DateTime Timestamp { get; set; }
        public List<BetModel> AllBets { get; set; }
        public Color WonColor { get; set; }
        public int WonNumber { get; set; }
    }
}