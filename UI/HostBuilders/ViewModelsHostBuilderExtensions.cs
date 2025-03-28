using Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UI.State.Authenticators;
using UI.State.Navigators;
using UI.ViewModels;
using UI.ViewModels.Factories;

namespace UI.HostBuilders;

public static class ViewModelsHostBuilderExtensions
{
    public static IHostBuilder AddViewModels(this IHostBuilder host)
    {
        host.ConfigureServices(services =>
        {
            services.AddTransient(CreateHomeViewModel);
            services.AddTransient<MainViewModel>();
        
            services.AddSingleton<IViewModelAbstractFactory, ViewModelAbstractFactory>();
            services.AddSingleton<CreateViewModel<HomeViewModel>>(services => services.GetRequiredService<HomeViewModel>);
            services.AddSingleton<CreateViewModel<LoginViewModel>>(services => () => CreateLoginViewModel(services));
            services.AddSingleton<CreateViewModel<RegisterViewModel>>(services => () => CreateRegisterViewModel(services));
            services.AddSingleton<CreateViewModel<DeliveryFormViewModel>>(services => () => CreateDeliveryFormViewModel(services));
            services.AddSingleton<CreateViewModel<CourierHomeViewModel>>(services => () => CreateCourierHomeViewModel(services));
            services.AddSingleton<CreateViewModel<UserProfileViewModel>>(services => () => CreateUserProfileViewModel(services));
            services.AddSingleton<CreateViewModel<UsersViewModel>>(services => () => СreateUsersViewModel(services));

            services.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();
            services.AddSingleton<ViewModelDelegateRenavigator<LoginViewModel>>();
            services.AddSingleton<ViewModelDelegateRenavigator<RegisterViewModel>>();
            services.AddSingleton<ViewModelDelegateRenavigator<DeliveryFormViewModel>>();
            services.AddSingleton<ViewModelDelegateRenavigator<CourierHomeViewModel>>();
            services.AddSingleton<ViewModelDelegateRenavigator<UserProfileViewModel>>();
        });

        return host;
    }

    private static HomeViewModel CreateHomeViewModel(IServiceProvider services)
    {
        return new HomeViewModel(
            services.GetRequiredService<IAuthenticator>(),
            services.GetRequiredService<IDeliveryInfoService>(),
            services.GetRequiredService<INavigator>());
    }
    
    private static LoginViewModel CreateLoginViewModel(IServiceProvider services)
    { 
        return new LoginViewModel(
            services.GetRequiredService<IAuthenticator>(),
            services.GetRequiredService<ViewModelDelegateRenavigator<HomeViewModel>>(),
            services.GetRequiredService<INavigator>());
    }

    private static RegisterViewModel CreateRegisterViewModel(IServiceProvider services)
    {
        return new RegisterViewModel(
            services.GetRequiredService<IAuthenticator>(), 
            services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>());
    }
    
    private static DeliveryFormViewModel CreateDeliveryFormViewModel(IServiceProvider services)
    {
        return new DeliveryFormViewModel(
            services.GetRequiredService<IUserDeliveryInfoService>(),
            services.GetRequiredService<IAuthenticator>(),
            services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>());
    }
    
    private static CourierHomeViewModel CreateCourierHomeViewModel(IServiceProvider services)
    {
        return new CourierHomeViewModel(
            services.GetRequiredService<IAuthenticator>(),
            services.GetRequiredService<IUserDeliveryInfoService>());
    }
    
    private static UserProfileViewModel CreateUserProfileViewModel(IServiceProvider services)
    {
        return new UserProfileViewModel(
            services.GetRequiredService<IAuthenticator>(),
            services.GetRequiredService<IUserService>(),
            services.GetRequiredService<IProfileImageService>(),
            services.GetRequiredService<INavigator>());
    }

    private static UsersViewModel СreateUsersViewModel(IServiceProvider services)
    {
        return new UsersViewModel(services.GetRequiredService<IUserService>());
    }
}