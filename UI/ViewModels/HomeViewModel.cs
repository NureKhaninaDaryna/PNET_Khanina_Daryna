using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Application.Services.Interfaces;
using Domain.Models;
using Domain.Shared.Results;
using UI.Commands;
using UI.Helpers;
using UI.State.Authenticators;
using UI.State.Navigators;

namespace UI.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private DateTime _displayedDate = DateTime.Today;
    private ObservableCollection<KeyValuePair<DeliveryInfo, string>> _colorDays;
    private bool _isColorDaysEmpty;
    
    public IAuthenticator Authenticator { get; }
    
    public INavigator Navigator { get; }
    
    public ObservableCollection<KeyValuePair<DeliveryInfo, string>> ColorDays
    {
        get => _colorDays;
        set
        {
            _colorDays = value;
            OnPropertyChanged(nameof(ColorDays));
            OnPropertyChanged(nameof(IsColorDaysEmpty));
        }
    }
    
    public bool IsColorDaysEmpty
    {
        get => !_colorDays.Any();
        private set
        {
            _isColorDaysEmpty = value;
            OnPropertyChanged(nameof(IsColorDaysEmpty));
        }
    }

    public DateTime DisplayedDate
    {
        get => _displayedDate;
        set
        {
            _displayedDate = value;
            OnPropertyChanged(nameof(DisplayedDate));
        }
    }

    public ICommand GenerateDeliveryDaysCommand { get; set; }
    
    public HomeViewModel(
        IAuthenticator authenticator, 
        IDeliveryInfoService deliveryInfoService, 
        INavigator navigator)
    {
        Authenticator = authenticator;
        Navigator = navigator;
        GenerateDeliveryDaysCommand = new GenerateDeliveryDaysCommand(this, authenticator, deliveryInfoService);
        ColorDays = new ObservableCollection<KeyValuePair<DeliveryInfo, string>>();
    }
}