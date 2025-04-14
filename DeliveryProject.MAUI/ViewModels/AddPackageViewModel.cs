using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Models;

namespace DeliveryProject.MAUI.ViewModels;

public partial class AddPackageViewModel : ObservableObject
{
    [ObservableProperty]
    private Package package = new();
    
    [ObservableProperty]
    private string? errorMessage;

    private readonly Popup _popup;

    public AddPackageViewModel(Popup popup)
    {
        _popup = popup;
    }

    public ICommand ConfirmCommand => new Command(() =>
    {
        if (!Validate())
            return;
        
        _popup.Close(Package); 
    });

    public ICommand CancelCommand => new Command(() =>
    {
        _popup.Close(null); 
    });
    
    private bool Validate()
    {
        if (Package.Weight <= 0)
        {
            ErrorMessage = "Weight must be greater than 0.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(Package.Dimensions))
        {
            ErrorMessage = "Dimensions cannot be empty.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(Package.Content))
        {
            ErrorMessage = "Content cannot be empty.";
            return false;
        }

        if (Package.Price <= 0)
        {
            ErrorMessage = "Price must be greater than 0.";
            return false;
        }

        ErrorMessage = null; // Clear error if validation passes
        return true;
    }
}