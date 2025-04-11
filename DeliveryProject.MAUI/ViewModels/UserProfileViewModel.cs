using System.Windows.Input;
using Application.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DeliveryProject.MAUI.State.Authenticators;
using Domain.Models;

namespace DeliveryProject.MAUI.ViewModels;


public partial class UserProfileViewModel : ObservableObject
{
    private readonly IUserService _userService;
    private readonly IProfileImageService _profileImageService;

    [ObservableProperty] private User user;
    [ObservableProperty] private ImageSource avatarSource;

    public IAuthenticator Authenticator { get; }

    public ICommand UpdateUserCommand { get; }
    public ICommand UploadPhotoCommand { get; }

    public UserProfileViewModel(
        IAuthenticator authenticator,
        IUserService userService,
        IProfileImageService profileImageService)
    {
        _userService = userService;
        _profileImageService = profileImageService;

        Authenticator = authenticator;

        if (authenticator.IsLoggedIn)
        {
            User = authenticator.CurrentUser!;
            LoadImage();
        }
        else
        {
            _ = GoToLoginPage();
        }

        UpdateUserCommand = new AsyncRelayCommand(UpdateUserAsync);
        UploadPhotoCommand = new AsyncRelayCommand(UploadAvatarAsync);
    }

    private async Task GoToLoginPage()
    {
        await Shell.Current.GoToAsync("login");
    }
    
    private void LoadImage()
    {
        if (User?.Avatar?.ImageData != null)
        {
            AvatarSource = ImageSource.FromUri(new Uri(User.Avatar.ImageData));
        }
        else
        {
            AvatarSource = "default_avatar.png";
        }
    }

    private async Task UpdateUserAsync()
    {
        var updated = await _userService.UpdateUser(User, User.Email);
    }

    private async Task UploadAvatarAsync()
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Select an image",
            FileTypes = FilePickerFileType.Images
        });

        if (result != null)
        {
            var fileName = result.FileName;
            var stream = await result.OpenReadAsync();
            
            var cachePath = Path.Combine(FileSystem.CacheDirectory, fileName);

            await using (var fileStream = File.Create(cachePath))
            {
                await stream.CopyToAsync(fileStream);
            }
            
            AvatarSource = ImageSource.FromFile(cachePath);
            
            var uploadResult = await _profileImageService.CreateImage(cachePath, User.Id);
        }
    }
}