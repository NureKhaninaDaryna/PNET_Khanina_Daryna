using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Application.Services.Interfaces;
using Domain.Models;
using UI.Commands;
using UI.Helpers;
using UI.State.Authenticators;
using UI.State.Navigators;

namespace UI.ViewModels;

public class UserProfileViewModel : ViewModelBase
{
    private User _user;
    private ImageSource? _source;
    private string _avatarFilePath;

    public User User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged(nameof(User));
        }
    }
    
    public string CurrentEmail { get; set; }
    
    public string CurrentAvatarPath { get; set; }

    public ICommand UpdateUserCommand { get; set; }
    
    public ICommand UploadPhotoCommand { get; set; }
    
    public IAuthenticator Authenticator { get; }
    
    public DateHelper DateHelper { get; set; }
    
    public ImageSource? Source
    {
        get => _source;
        set
        {
            _source = value;
            OnPropertyChanged(nameof(Source));
        }
    }

    public string AvatarFilePath
    {
        get => _avatarFilePath;
        set
        {
            _avatarFilePath = value;
            OnPropertyChanged(nameof(AvatarFilePath));
        }
    }

    public UserProfileViewModel(
        IAuthenticator authenticator, 
        IUserService userService, 
        IProfileImageService profileImageService,
        INavigator navigator)
    {
        DateHelper = new DateHelper();
        Authenticator = authenticator;
        UpdateUserCommand = new UpdateUserCommand(this, userService, profileImageService, navigator);
        UploadPhotoCommand = new UploadPhotoCommand(this);
        
        if (!Authenticator.IsLoggedIn) return;
        
        User = Authenticator.CurrentUser;
        CurrentEmail = User.Email;

        if (Authenticator.CurrentUser.Avatar != null)
        {
            LoadImage();   
        }
    }

    private void LoadImage()
    {
        var bitmap = new BitmapImage();
            
        bitmap.BeginInit();
            
       // bitmap.UriSource = new Uri(Authenticator.CurrentUser.Avatar!.ImageData, UriKind.Absolute);
        // = Authenticator.CurrentUser.Avatar!.ImageData;
            
        bitmap.EndInit();
            
        Source = bitmap;
    }
}