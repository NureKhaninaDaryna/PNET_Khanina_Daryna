using DeliveryProject.MAUI.ViewModels;

namespace DeliveryProject.MAUI.Pages;

public partial class UsersPage : ContentPage
{
    public UsersPage(UsersViewModel usersViewModel)
    {
        InitializeComponent();
        BindingContext = usersViewModel;
    }
}