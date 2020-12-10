using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Roulette.Models
{
    public class BetModel
    {
        [Key] public ulong Id { get; set; }
        public float Amount { get; set; }
        public Color Color { get; set; }
        public string ConnectionId { get; set; }
        public string SteamID { get; set; }

        public string Serialize()
        {
            return string.Format("{0},{1},{2},{3}", SteamID, Color, Amount, ConnectionId);
        }

        public static BetModel Deserialize(string input)
        {
            var args = input.Split(',').ToList();
            if (!Enum.TryParse(args[1], out Color color))
                throw new Exception("could not deserialize Enum.Color from string");


            return new BetModel
                {SteamID = args[0], Color = color, Amount = float.Parse(args[2]), ConnectionId = args[3]};
        }
    }

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