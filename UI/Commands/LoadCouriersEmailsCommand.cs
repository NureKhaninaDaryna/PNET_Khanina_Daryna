using System.Windows.Input;
using Application.Services.Interfaces;
using UI.State.Authenticators;
using UI.ViewModels;

namespace UI.Commands;

public class LoadCouriersEmailsCommand : ICommand
{
    private readonly DeliveryFormViewModel _deliveryFormViewModel;
    private readonly IAuthenticator _authenticator;
    private readonly IUserDeliveryInfoService _userDeliveryInfoService;

    public LoadCouriersEmailsCommand(
        DeliveryFormViewModel deliveryFormViewModel,
        IAuthenticator authenticator,
        IUserDeliveryInfoService userDeliveryInfoService)
    {
        _deliveryFormViewModel = deliveryFormViewModel;
        _authenticator = authenticator;
        _userDeliveryInfoService = userDeliveryInfoService;
    }
    
    public bool CanExecute(object? parameter)
    {
        return _authenticator.IsLoggedIn;
    }

    public async void Execute(object? parameter)
    {
        var getAllCourierEmailsResult = await _userDeliveryInfoService.GetAllCourierEmails();
        if (getAllCourierEmailsResult is not { IsSuccess: true, Value: not null }) return;
        
        var getAllRecipientEmails = await _userDeliveryInfoService.GetAllRecipientEmails();
        if (getAllRecipientEmails is not { IsSuccess: true, Value: not null }) return;
        
        System.Windows.Application.Current.Dispatcher.Invoke(() =>
        {
            _deliveryFormViewModel.CourierEmails.Clear();
            foreach (var email in getAllCourierEmailsResult.Value)
            {
                _deliveryFormViewModel.CourierEmails.Add(email);
            }

            _deliveryFormViewModel.CourierEmails = _deliveryFormViewModel.CourierEmails;
            
            
            _deliveryFormViewModel.RecipientEmails.Clear();
            foreach (var email in getAllRecipientEmails.Value)
            {
                _deliveryFormViewModel.RecipientEmails.Add(email);
            }

            _deliveryFormViewModel.RecipientEmails = _deliveryFormViewModel.RecipientEmails;
        });
    }

    public event EventHandler? CanExecuteChanged;
}