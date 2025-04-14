using System.Collections.ObjectModel;
using System.Windows.Input;
using Application.Services.Interfaces;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DeliveryProject.MAUI.Pages;
using DeliveryProject.MAUI.State.Authenticators;
using Domain.Models;

namespace DeliveryProject.MAUI.ViewModels;

public partial class DeliveryFormViewModel : ObservableObject
{
    private readonly IUserDeliveryInfoService _userDeliveryInfoService;

    [ObservableProperty] private ObservableCollection<string> courierEmails = new();
    [ObservableProperty] private ObservableCollection<string> recipientEmails = new();
    [ObservableProperty] private string selectedRecipientEmail;
    [ObservableProperty] private string selectedCourierEmail;
    [ObservableProperty] private string startAddress;
    [ObservableProperty] private string deliveryAddress;
    [ObservableProperty] private string trackingNumber;
    [ObservableProperty] private ObservableCollection<string> allTrackingNumbers = new();

    public string AllTrackingNumbersText => string.Join("\n", AllTrackingNumbers);

    public ObservableCollection<Package> Packages { get; set; } = new();

    public int PackagesCount => Packages.Count;

    public IAuthenticator Authenticator { get; }

    public ICommand AddUserDeliveryInfoCommand { get; }
    public ICommand OpenAddPackageWindowCommand { get; }

    public DeliveryFormViewModel(IUserDeliveryInfoService userDeliveryInfoService, IAuthenticator authenticator)
    {
        _userDeliveryInfoService = userDeliveryInfoService;
        Authenticator = authenticator;

        if (!authenticator.IsLoggedIn)
        {
            _ = GoToLoginPage();
        }
        
        AddUserDeliveryInfoCommand = new AsyncRelayCommand(OnAddUserDeliveryInfo);
        OpenAddPackageWindowCommand = new AsyncRelayCommand(OnOpenAddPackageWindow);

        LoadDataAsync();
    }

    private async Task GoToLoginPage()
    {
        await Shell.Current.GoToAsync("login");
    }
    
    private async void LoadDataAsync()
    {
        if (Authenticator.CurrentUser == null)
        {
            await Shell.Current.GoToAsync("login");
        }
        
        var couriersResult = await _userDeliveryInfoService.GetAllCourierEmails();
        if (couriersResult.IsSuccess)
        {
            CourierEmails = new ObservableCollection<string>(couriersResult.Value);
        }

        var recipientsResult = await _userDeliveryInfoService.GetAllRecipientEmails();
        if (recipientsResult.IsSuccess)
        {
            RecipientEmails = new ObservableCollection<string>(recipientsResult.Value);
        }

        var trackingResult = await _userDeliveryInfoService.GetAllTrackingNumbersByUserEmail(Authenticator.CurrentUser?.Email!);

        if (trackingResult.IsSuccess)
        {
            AllTrackingNumbers = new ObservableCollection<string>(trackingResult.Value);

        }

        OnPropertyChanged(nameof(AllTrackingNumbersText));
    }

    private async Task OnAddUserDeliveryInfo()
    {
        if (string.IsNullOrWhiteSpace(StartAddress) || string.IsNullOrWhiteSpace(DeliveryAddress))
            return;

        var result = await _userDeliveryInfoService.CreateUserDeliveryInfo(SelectedCourierEmail, SelectedRecipientEmail,
            StartAddress, DeliveryAddress, Authenticator.CurrentUser, Packages.ToList());

        if (result.IsSuccess)
        {
            AllTrackingNumbers.Add(result.Value.TrackingNumber);   
        }
    }

    private async Task OnOpenAddPackageWindow()
    {
        var popup = new AddPackagePage();
        var vm = new AddPackageViewModel(popup);
        popup.BindingContext = vm;

        var result = await Shell.Current.CurrentPage.ShowPopupAsync(popup);

        if (result is Package package)
        {
            Packages.Add(package);
            OnPropertyChanged(nameof(PackagesCount));
        }
    }
}
