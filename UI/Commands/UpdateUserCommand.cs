using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Application.Services.Interfaces;
using UI.State.Authenticators;
using UI.State.Navigators;
using UI.ViewModels;

namespace UI.Commands;

public class UpdateUserCommand : ICommand
{
    private readonly UserProfileViewModel _userProfileViewModel;
    private readonly IUserService _userService;
    private readonly IProfileImageService _profileImageService;
    private readonly INavigator _navigator;

    public UpdateUserCommand(
        UserProfileViewModel userProfileViewModel, 
        IUserService userService,
        IProfileImageService profileImageService,
        INavigator navigator)
    {
        _userProfileViewModel = userProfileViewModel;
        _userService = userService;
        _profileImageService = profileImageService;
        _navigator = navigator;
    }
    
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public async void Execute(object? parameter)
    {
        var updatedUser = _userProfileViewModel.User;
        
        if (_userProfileViewModel is { Source: not null, AvatarFilePath: not null })
        {
            var updateAvatarResult = await _profileImageService.CreateImage(_userProfileViewModel.AvatarFilePath, updatedUser.Id);

            if (updateAvatarResult is not { IsSuccess: true, Value: not null })
            {
                MessageBox.Show(updateAvatarResult.Error.Message);
                
                return;
            }
            
            _navigator.UpdateAvatar();
        }
        
        var updateResult = await _userService.UpdateUser(updatedUser, _userProfileViewModel.CurrentEmail);

        if (updateResult is not { IsSuccess: true, Value: not null })
        {
            MessageBox.Show(updateResult.Error.Message);
        }
        else
        {
            _userProfileViewModel.CurrentEmail = updatedUser.Email;
            
            MessageBox.Show("You successfully updated the profile info");   
        }
    }

    public event EventHandler? CanExecuteChanged;
}