using Application.Services.Implementations;
using Application.Services.Interfaces;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using BlazorUI.Components;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login"; 
        options.AccessDeniedPath = "/access-denied";
    });

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5210/") });

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<DbContext, AppDbContext>();
        
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer("data source=localhost;Database=DeliverySqlDb;TrustServerCertificate=true;Integrated Security=True;");
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();