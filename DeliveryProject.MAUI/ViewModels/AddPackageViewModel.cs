using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Models;

namespace DeliveryProject.MAUI.ViewModels;

public partial class AddPackageViewModel : ObservableObject
{
    [ObservableProperty]
    private Package package = new();

    private readonly Popup _popup;

    public AddPackageViewModel(Popup popup)
    {
        _popup = popup;
    }

    public ICommand ConfirmCommand => new Command(() =>
    {
        _popup.Close(Package); 
    });

    public ICommand CancelCommand => new Command(() =>
    {
        _popup.Close(null); 
    });
}