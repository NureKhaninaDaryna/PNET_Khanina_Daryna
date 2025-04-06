using Application.Services.Implementations;
using Application.Services.Interfaces;
using DeliveryProject.MAUI.Pages;
using DeliveryProject.MAUI.State.Authenticators;
using DeliveryProject.MAUI.ViewModels;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DeliveryProject.MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        
        builder.Services.AddScoped<DbContext, AppDbContext>();
        
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer("data source=192.168.1.3,1433;Database=DeliverySqlDb;User Id=MauiUser;Password=darhan1168;TrustServerCertificate=true;");
            options.UseLazyLoadingProxies();
        });
        
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddSingleton<IPasswordHashing, PasswordHashing>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IJwtService, JwtService>();
        builder.Services.AddScoped<ISignInService, SignInService>();
        builder.Services.AddScoped<IRegistrationService, RegistrationService>();
        builder.Services.AddScoped<IDeliveryInfoService, DeliveryInfoService>();
        builder.Services.AddScoped<IUserDeliveryInfoService, UserDeliveryInfoService>();
        builder.Services.AddScoped<IFileAddingService, FileAddingService>();
        builder.Services.AddScoped<IProfileImageService, ProfileImageService>();
        
        builder.Services.AddSingleton<IAuthenticator, Authenticator>();
        
        builder.Services.AddTransient<RegisterViewModel>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddTransient<LoginPage>();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}