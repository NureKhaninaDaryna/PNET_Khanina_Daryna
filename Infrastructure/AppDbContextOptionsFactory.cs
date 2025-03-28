using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure;

public class AppDbContextOptionsFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer("data source=khanina-d;Database=DeliveryDb;TrustServerCertificate=true;Integrated Security=True;");

        return new AppDbContext(optionsBuilder.Options);
    }
}
