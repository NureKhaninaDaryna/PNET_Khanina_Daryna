using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UI.State.Authenticators;
using UI.State.Navigators;

namespace UI.HostBuilders;

public static class StoresHostBuilderExtensions
{
    public static IHostBuilder AddStores(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton<IAuthenticator, Authenticator>();
        });

        return host;
    }
}