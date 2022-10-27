using Microsoft.EntityFrameworkCore;
using ROS.Entity;

namespace ROS.DataBase;

public class ChequeContext : DbContext
{
    public DbSet<Cheque> Cheques { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Shop> Shops { get; set; } = null!;
    
    public ChequeContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=cheque;Username=postgres;Password=Last");
    }
}