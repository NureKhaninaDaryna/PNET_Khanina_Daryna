using System.Collections.ObjectModel;
using System.Windows.Input;
using Application.Services.Interfaces;
using Domain.Models;
using UI.Commands;
using UI.State.Authenticators;
using UI.State.Navigators;
using UI.Views;

namespace UI.ViewModels;

public class DeliveryFormViewModel : ViewModelBase
{
    private ObservableCollection<string> _courierEmails;
    
    public ObservableCollection<string> CourierEmails
    {
        get => _courierEmails;
        set 
        { 
            _courierEmails = value; 
            OnPropertyChanged(nameof(CourierEmails));
        }
    }
    
    private ObservableCollection<string> _recipientEmails;
    
    public ObservableCollection<string> RecipientEmails
    {
        get => _recipientEmails;
        set 
        { 
            _recipientEmails = value; 
            OnPropertyChanged(nameof(RecipientEmails));
        }
    }
    
    private string _selectedRecipientEmail;
    
    public string SelectedRecipientEmail
    {
        get => _selectedRecipientEmail;
        set 
        { 
            _selectedRecipientEmail = value; 
            OnPropertyChanged(nameof(SelectedRecipientEmail));
        }
    }
    
    private string _selectedCourierEmail;
    
    public string SelectedCourierEmail
    {
        get => _selectedCourierEmail;
        set 
        { 
            _selectedCourierEmail = value; 
            OnPropertyChanged(nameof(SelectedCourierEmail));
        }
    }
    
    private string _startAddress;
    
    public string StartAddress 
    {
        get => _startAddress;
        set 
        { 
            _startAddress = value; 
            OnPropertyChanged(nameof(StartAddress));
        }
    }
    
    private string _deliveryAddress;
    
    public string DeliveryAddress 
    {
        get => _deliveryAddress;
        set 
        { 
            _deliveryAddress = value; 
            OnPropertyChanged(nameof(DeliveryAddress));
        }
    }

    private string _trackingNumber;
    
    public string TrackingNumber 
    {
        get => _trackingNumber;
        set 
        { 
            _trackingNumber = value; 
            OnPropertyChanged(nameof(TrackingNumber));
        }
    }
    
    private ObservableCollection<string> _allTrackingNumbers;
    
    public ObservableCollection<string> AllTrackingNumbers 
    {
        get => _allTrackingNumbers;
        set 
        { 
            _allTrackingNumbers = value; 
            OnPropertyChanged(nameof(AllTrackingNumbers));
            OnPropertyChanged(nameof(AllTrackingNumbersText));
        }
    }
    
    public string AllTrackingNumbersText => string.Join(Environment.NewLine, AllTrackingNumbers);
    
    public AddUserDeliveryInfoCommand AddUserDeliveryInfoCommand { get; }
    
    public IAuthenticator Authenticator { get; }

    public ICommand LoadCouriersEmailsCommand { get; set; }
    
    public ICommand LoadAllExistedTrackingNumberByUserCommand { get; set; }
    
    public ObservableCollection<Package> Packages { get; set; } = new();

    public int PackagesCount => Packages.Count;

    public ICommand OpenAddPackageWindowCommand { get; }
    
    public DeliveryFormViewModel(
        IUserDeliveryInfoService userDeliveryInfoService, 
        IAuthenticator authenticator, 
        IRenavigator registerRenavigator)
    {
        CourierEmails = [];
        RecipientEmails = [];
        AllTrackingNumbers = [];
        Authenticator = authenticator;
        AddUserDeliveryInfoCommand = new AddUserDeliveryInfoCommand(this, userDeliveryInfoService, authenticator, registerRenavigator);
        LoadCouriersEmailsCommand = new LoadCouriersEmailsCommand(this, authenticator, userDeliveryInfoService);
        LoadAllExistedTrackingNumberByUserCommand = new LoadAllExistedTrackingNumberByUserCommand(this, authenticator, userDeliveryInfoService);
        OpenAddPackageWindowCommand = new RelayCommand(OpenAddPackageWindow);

        InitializeAsync();
    }
    
    private async void InitializeAsync()
    {
        if (LoadCouriersEmailsCommand.CanExecute(null))
        {
            await Task.Run(() => LoadCouriersEmailsCommand.Execute(null));
        }

        if (LoadAllExistedTrackingNumberByUserCommand.CanExecute(null))
        {
            await Task.Run(() => LoadAllExistedTrackingNumberByUserCommand.Execute(null));
        }
    }
    
    private void OpenAddPackageWindow()
    {
        var addPackageWindow = new AddPackageWindow();
        if (addPackageWindow.ShowDialog() == true)
        {
            Packages.Add(addPackageWindow.NewPackage);
            OnPropertyChanged(nameof(PackagesCount));
        }
    }
}