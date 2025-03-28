using System.Windows.Input;
using Application.Services.Interfaces;
using UI.State.Authenticators;
using UI.ViewModels;

namespace UI.Commands;

public class LoadAllExistedTrackingNumberByUserCommand : ICommand
{
    private readonly DeliveryFormViewModel _deliveryFormViewModel;
    private readonly IUserDeliveryInfoService _userDeliveryInfoService;
    private readonly IAuthenticator _authenticator;

    public LoadAllExistedTrackingNumberByUserCommand(
        DeliveryFormViewModel deliveryFormViewModel,
        IAuthenticator authenticator,
        IUserDeliveryInfoService userDeliveryInfoService)
    {
        _deliveryFormViewModel = deliveryFormViewModel;
        _userDeliveryInfoService = userDeliveryInfoService;
        _authenticator = authenticator;
    }

    public bool CanExecute(object? parameter)
    {
        return _authenticator.IsLoggedIn;
    }

    public async void Execute(object? parameter)
    {
        var result = await _userDeliveryInfoService.GetAllTrackingNumbersByUserEmail(_authenticator.CurrentUser.Email);

        if (result is not { IsSuccess: true, Value: not null }) return;
        
        System.Windows.Application.Current.Dispatcher.Invoke(() =>
        {
            foreach (var trackingNumbers in result.Value)
            {
                _deliveryFormViewModel.AllTrackingNumbers.Add(trackingNumbers);
            }

            _deliveryFormViewModel.AllTrackingNumbers = _deliveryFormViewModel.AllTrackingNumbers;
        });
    }

    public event EventHandler? CanExecuteChanged;
}