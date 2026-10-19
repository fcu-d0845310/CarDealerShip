using CarDealerShip.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealerShip.Data
{
    public class AppDbContext : DbContext
    {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

            public DbSet<Role> Roles { get; set; }
            public DbSet<User> Users { get; set; }
            public DbSet<Branch> Branches { get; set; }
            public DbSet<Car> Cars { get; set; }
            public DbSet<CarImage> CarImages { get; set; }
            public DbSet<UserFavorite> UserFavorites { get; set; }
            public DbSet<Reservation> Reservations { get; set; }
            public DbSet<Offer> Offers { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<TradeInRequest> TradeInRequests { get; set; }
            public DbSet<TradeImage> TradeImages { get; set; }
            public DbSet<Post> Posts { get; set; }
            public DbSet<Comment> Comments { get; set; }
            public DbSet<SalesRecord> SalesRecords { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // 避免 SQL Server 多重連鎖刪除路徑報錯
                foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                {
                    relationship.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
    
    }
}
