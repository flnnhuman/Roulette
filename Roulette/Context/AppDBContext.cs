using Microsoft.EntityFrameworkCore;
using Roulette.Models;

namespace Roulette.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<SteamUsersModel> SteamUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SteamUsersModel>().HasData(new SteamUsersModel
            {
                SteamID = "76561100000000000",
                Balance = 0,
                avatar = "https://cdn.cloudflare.steamstatic.com/steamcommunity/public/images/avatars/cb/.jpg",
                avatarfull = "https://cdn.cloudflare.steamstatic.com/steamcommunity/public/images/avatars/cb/_full.jpg",
                avatarmedium =
                    "https://cdn.cloudflare.steamstatic.com/steamcommunity/public/images/avatars/cb/_medium.jpg",
                communityvisibilitystate = 3,
                personaname = "name",
                personastate = 0,
                personastateflags = 0,
                profilestate = 1, profileurl = "https://steamcommunity.com/id/"
            });
        }
    }
}