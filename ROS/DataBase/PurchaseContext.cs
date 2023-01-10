using Microsoft.EntityFrameworkCore;
using ROS.Entity;

namespace ROS.DataBase;

public class PurchaseContext : DbContext
{
    public DbSet<Cheque> cheques { get; set; }
    public DbSet<Product> products { get; set; }
    public DbSet<Shop> shops { get; set; } = null!;
    
    public PurchaseContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=cheque;Username=postgres;Password=Last");
    }
}