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
        public DbSet<GameModel> GamesHistory { get; set; }
        public DbSet<MessageModel> ChatHistory { get; set; }
        public DbSet<ReferralModel> ReferralModels { get; set; }
        public DbSet<SettingsModel> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SteamUsersModel>()
                .HasOne(a => a.Referral)
                .WithOne(b => b.SteamUsers)
                .HasForeignKey<ReferralModel>(b => b.SteamUsersId);
            modelBuilder.Entity<SettingsModel>().HasData(new SettingsModel()
            {
                ChatEnable = true,
                NextRoll = -1,
                id = 1
            });
        }
    }
}