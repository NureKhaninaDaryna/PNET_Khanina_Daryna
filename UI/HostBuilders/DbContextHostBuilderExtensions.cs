using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UI.HostBuilders;

public static class DbContextHostBuilderExtensions
{
    public static IHostBuilder AddDbContext(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddScoped<DbContext, AppDbContext>();
        
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("data source=localhost;Database=DeliveryDb;TrustServerCertificate=true;Integrated Security=True;");
                options.UseLazyLoadingProxies();
            });
        });

        return host;
    }
}