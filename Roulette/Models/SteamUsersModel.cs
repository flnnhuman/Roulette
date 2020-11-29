using System.ComponentModel.DataAnnotations;

namespace Roulette.Models
{
    public class SteamUsersModel
    {
        [Key] public string SteamID { get; set; }
    }
}