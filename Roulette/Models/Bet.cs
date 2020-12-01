using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Roulette.Models
{
    public class Bet
    {
        public string SteamID;
        public Color Color;
        public float amount;
        public string ConnectionId;

        public string Serialize()
        {
            return string.Format("{0},{1},{2},{3}", SteamID, Color, amount, ConnectionId);
        }

        public static Bet Deserialize(string input)
        {
            var args = input.Split(',').ToList();
            if (!Enum.TryParse(args[1], out Color color))
            {
                throw new Exception("could not deserialize Enum.Color from string");
            }


            return new Bet {SteamID = args[0], Color = color, amount = float.Parse(args[2]), ConnectionId = args[3]};
        }
    }

    public enum Color
    {
        Red = 1,
        Green = 2,
        Black = 3
    }
    public class BetColors
    {
        public static List<int> Red = new List<int>(){1,2,3,4,5,6,7};
        public static List<int> Black = new List<int>(){8,9,10,11,12,13,14};
    }
}