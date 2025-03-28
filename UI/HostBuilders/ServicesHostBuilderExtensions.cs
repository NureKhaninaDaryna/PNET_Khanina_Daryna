using Application.Services.Implementations;
using Application.Services.Interfaces;
using Domain.Models;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace UI.HostBuilders;

public static class ServicesHostBuilderExtensions
{
    public static IHostBuilder AddServices(this IHostBuilder host)
    {
        var connectionString = "mongodb://admin:Test123!@localhost:27017/DeliveryDb?authSource=admin";
        var mongoClient = new MongoClient(connectionString);
        var database = mongoClient.GetDatabase("DeliveryDb");
        
        host.ConfigureServices(services =>
        {
            services.AddSingleton<IMongoClient>(new MongoClient(connectionString));
            
            services.AddScoped<IMongoDatabase>(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase("DeliveryDb");
            });
            
            services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));
            services.AddSingleton<IPasswordHashing, PasswordHashing>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IDeliveryInfoService, DeliveryInfoService>();
            services.AddScoped<IUserDeliveryInfoService, UserDeliveryInfoService>();
            services.AddScoped<IFileAddingService, FileAddingService>();
            services.AddScoped<IProfileImageService, ProfileImageService>();
            //services.AddSingleton<IRepository<User>>(new MongoRepository<User>(database, "Users"));
            services.AddSingleton<IRepository<DeliveryInfo>>(new MongoRepository<DeliveryInfo>(database, "DeliveryInfos"));
            services.AddSingleton<IRepository<ProfileImage>>(new MongoRepository<ProfileImage>(database, "ProfileImages"));
            services.AddSingleton<IRepository<Package>>(new MongoRepository<Package>(database, "Packages"));
            
            services.AddScoped<UserRepository>();
            services.AddScoped<IUserRepository>(sp => sp.GetRequiredService<UserRepository>());
            services.AddScoped<IRepository<User>>(sp => sp.GetRequiredService<UserRepository>());
        });

        return host;
    }
}