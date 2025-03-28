using System.Windows;
using System.Windows.Input;
using Application.Services.Interfaces;
using Domain.Models;
using UI.Helpers;
using UI.State.Authenticators;
using UI.ViewModels;

namespace UI.Commands;

public class GenerateDeliveryDaysCommand : ICommand
{
    private readonly HomeViewModel _homeViewModel;
    private readonly IAuthenticator _authenticator;
    private readonly IDeliveryInfoService _deliveryInfoService;

    public GenerateDeliveryDaysCommand(
        HomeViewModel homeViewModel, 
        IAuthenticator authenticator, 
        IDeliveryInfoService deliveryInfoService)
    {
        _homeViewModel = homeViewModel;
        _authenticator = authenticator;
        _deliveryInfoService = deliveryInfoService;
    }
    
    public bool CanExecute(object? parameter)
    {
        return _authenticator.IsLoggedIn;
    }

    public async void Execute(object? parameter)
    {
        MessageBox.Show("If the cell with the number is blue, it indicates a delivery, and if it's red, it means a pickup.\n");
        
        var colorDays = new Dictionary<DeliveryInfo, string?>();
        var deliveryInfosResult = await _deliveryInfoService.GetAllDeliveryInfos();
        
        if (!deliveryInfosResult.IsSuccess) MessageBox.Show(deliveryInfosResult.Error.Message);
        
        colorDays.SetColorsForDate(deliveryInfosResult.Value);
        
        colorDays = colorDays.OrderBy(kv => kv.Key.Date)
            .ToDictionary(kv => kv.Key, kv => kv.Value);
        
        _homeViewModel.ColorDays.Clear();
        
        foreach (var kvp in colorDays)
        {
            _homeViewModel.ColorDays.Add(kvp);
        }

        _homeViewModel.ColorDays = _homeViewModel.ColorDays;
    }

    public event EventHandler? CanExecuteChanged;
}