using System;
using System.ComponentModel.DataAnnotations;

namespace Roulette.Models
{
    public class MessageModel
    {
        [Key] public ulong Id { get; set; }

        public string SteamId { get; set; }
        public string Avatar { get; set; }
        public string PersonaName { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}