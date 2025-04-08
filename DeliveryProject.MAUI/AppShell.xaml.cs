using DeliveryProject.MAUI.Pages;
using DeliveryProject.MAUI.State.Authenticators;

namespace DeliveryProject.MAUI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("register", typeof(RegisterPage));
        Routing.RegisterRoute("login", typeof(LoginPage));
        Routing.RegisterRoute("main", typeof(MainPage));
        Routing.RegisterRoute("delivery", typeof(DeliveryForm));
        Routing.RegisterRoute("addPackage", typeof(AddPackagePage));
    }
}
