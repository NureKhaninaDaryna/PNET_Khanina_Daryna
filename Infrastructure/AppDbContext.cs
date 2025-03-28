using Domain.Models;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<DeliveryInfo> DeliveryInfos { get; set; }

    //public DbSet<UserDeliveryInfo> UserDeliveryInfos { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("data source=localhost;Database=DeliveryDb;TrustServerCertificate=true;Integrated Security=True;");
        }
        
        base.OnConfiguring(optionsBuilder);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
    }
}