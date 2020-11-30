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
                Balance = 0
            });
        }
    }
}