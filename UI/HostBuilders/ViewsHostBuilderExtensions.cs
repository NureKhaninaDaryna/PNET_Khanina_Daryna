using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UI.ViewModels;

namespace UI.HostBuilders;

public static class ViewsHostBuilderExtensions
{
    public static IHostBuilder AddViews(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddSingleton<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
        });

        return host;
    }
}