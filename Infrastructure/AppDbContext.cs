using Domain.Models;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ProfileImage> ProfileImages { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<DeliveryInfo> DeliveryInfos { get; set; }
    
    public DbSet<Package> Packages { get; set; }
    
    public DbSet<DeliveryStatusHistory> DeliveryStatusHistory { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DeliveryInfo>()
            .HasOne(d => d.Courier)
            .WithMany(u => u.CourierDeliveryInfos)
            .HasForeignKey(d => d.CourierId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DeliveryInfo>()
            .HasOne(d => d.Recipient)
            .WithMany(u => u.DeliveryInfos) 
            .HasForeignKey(d => d.RecipientId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<User>()
            .HasOne(u => u.Avatar)
            .WithOne(p => p.User)
            .HasForeignKey<User>(u => u.AvatarId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}