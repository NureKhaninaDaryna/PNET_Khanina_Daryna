using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UI.HostBuilders;

namespace UI;

public partial class App : System.Windows.Application
{
    private readonly IHost _host;

    public App()
    {
        _host = CreateHostBuilder().Build();
    }

    private static IHostBuilder CreateHostBuilder(string[] args = null)
    {
        return Host.CreateDefaultBuilder(args)
            .AddDbContext()
            .AddServices()
            .AddStores()
            .AddViewModels()
            .AddViews();;
    }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        _host.Start();
        
        Window window = _host.Services.GetRequiredService<MainWindow>();
        window.Show();
        
        base.OnStartup(e);
    }
    
    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();

        base.OnExit(e);
    }
}