using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Helpers;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        // var courier = new User("courier@gmail.com", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", UserRole.Courier);
        //
        // var deliveryInfos = new List<DeliveryInfo>()
        // {
        //     new (new DateOnly(2024, 05, 20), courier, true, false),
        //     new (new DateOnly(2024, 05, 21), courier, false, true)
        // };
        //
        // modelBuilder.Entity<User>().HasData(courier);
        // modelBuilder.Entity<DeliveryInfo>().HasData(deliveryInfos);
    }
}