using DeliveryProject.MAUI.Pages;
using DeliveryProject.MAUI.State.Authenticators;

namespace DeliveryProject.MAUI;

public partial class App : Microsoft.Maui.Controls.Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}