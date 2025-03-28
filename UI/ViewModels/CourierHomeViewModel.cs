using Application.Services.Interfaces;
using Domain.Models;
using UI.State.Authenticators;

namespace UI.ViewModels;

public class CourierHomeViewModel : ViewModelBase
{
    private readonly IUserDeliveryInfoService _userDeliveryInfoService;
    
    public List<DeliveryInfo> UserDeliveryInfos { get; private set; }
    
    public CourierHomeViewModel(IAuthenticator authenticator, IUserDeliveryInfoService userDeliveryInfoService)
    {
        _userDeliveryInfoService = userDeliveryInfoService;
        
        if (authenticator.IsLoggedIn)
        {
            LoadUserDeliveryInfos(authenticator.CurrentUser.Email);
        }
    }

    private void LoadUserDeliveryInfos(string email)
    {
        UserDeliveryInfos = [];

        var userDeliveryInfosResult = _userDeliveryInfoService.GetAllUserDeliveryInfoByCourier(email);

        if (userDeliveryInfosResult.IsSuccess)
        {
            UserDeliveryInfos = userDeliveryInfosResult.Value;
        }
    }
}