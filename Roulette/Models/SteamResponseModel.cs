using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Roulette.Models
{
    [JsonArray]
    public class SteamResponseModel : ICollection<Player>
    {
        public SteamResponseModel()
        {
            Players = new List<Player>();
        }

        protected SteamResponseModel(ICollection<Player> collection)
        {
            Players = collection;
        }

        public ICollection<Player> Players { get; set; }

        public void Add(Player item)
        {
            Players.Add(item);
        }

        public void Clear()
        {
            Players.Clear();
        }

        public bool Contains(Player item)
        {
            return Players.Contains(item);
        }

        public void CopyTo(Player[] array, int arrayIndex)
        {
            Players.CopyTo(array, arrayIndex);
        }

        public int Count => Players.Count;

        public bool IsReadOnly => false;

        public bool Remove(Player item)
        {
            return Players.Remove(item);
        }

        public IEnumerator<Player> GetEnumerator()
        {
            return Players.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Players.GetEnumerator();
        }
    }

    public class Player
    {
        public string steamid { get; set; }
        public int communityvisibilitystate { get; set; }
        public int profilestate { get; set; }
        public string personaname { get; set; }
        public int commentpermission { get; set; }
        public string profileurl { get; set; }
        public string avatar { get; set; }
        public string avatarmedium { get; set; }
        public string avatarfull { get; set; }
        public string avatarhash { get; set; }
        public int lastlogoff { get; set; }
        public int personastate { get; set; }
        public string realname { get; set; }
        public string primaryclanid { get; set; }
        public int timecreated { get; set; }
        public int personastateflags { get; set; }
    }
}