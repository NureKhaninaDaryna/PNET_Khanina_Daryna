using System.Windows;
using System.Windows.Input;
using Application.Services.Interfaces;
using Domain.Shared.Errors;
using UI.State.Authenticators;
using UI.State.Navigators;
using UI.ViewModels;

namespace UI.Commands;

public class AddUserDeliveryInfoCommand : ICommand
{
    private readonly DeliveryFormViewModel _deliveryFormViewModel;
    private readonly IUserDeliveryInfoService _userDeliveryInfoService;
    private readonly IAuthenticator _authenticator;
    private readonly IRenavigator _registerRenavigator;

    public AddUserDeliveryInfoCommand(
        DeliveryFormViewModel deliveryFormViewModel, 
        IUserDeliveryInfoService userDeliveryInfoService,
        IAuthenticator authenticator,
        IRenavigator registerRenavigator)
    {
        _deliveryFormViewModel = deliveryFormViewModel;
        _userDeliveryInfoService = userDeliveryInfoService;
        _authenticator = authenticator;
        _registerRenavigator = registerRenavigator;
    }
    
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public async void Execute(object? parameter)
    {
        var result = await _userDeliveryInfoService.CreateUserDeliveryInfo(
            _deliveryFormViewModel.SelectedCourierEmail, _deliveryFormViewModel.SelectedRecipientEmail, 
            _deliveryFormViewModel.StartAddress, _deliveryFormViewModel.DeliveryAddress, _authenticator.CurrentUser,
            _deliveryFormViewModel.Packages.ToList());

        if (!result.IsSuccess)
        {
            MessageBox.Show(result.Error.Message);
            
            if (result.Error == ServiceErrors.NotAuthenticate)
            {
                _registerRenavigator.Renavigate();
            }
        }
        else
        {
            _deliveryFormViewModel.AllTrackingNumbers.Add(result.Value.TrackingNumber);
            _deliveryFormViewModel.AllTrackingNumbers = _deliveryFormViewModel.AllTrackingNumbers;
            
            MessageBox.Show("You successfully sent this info");
        }
    }

    public event EventHandler? CanExecuteChanged;
}