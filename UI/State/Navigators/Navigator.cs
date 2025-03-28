using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using UI.Commands;
using UI.Enums;
using UI.Models;
using UI.State.Authenticators;
using UI.ViewModels;
using UI.ViewModels.Factories;

namespace UI.State.Navigators;

public class Navigator : ObservableObject, INavigator
{
    private ImageSource? _source;
    
    private readonly IViewModelAbstractFactory _viewModelAbstractFactory;
    private ViewModelBase _currentViewModel;
    
    public IAuthenticator Authenticator { get; }

    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
    
    public string CurrentAvatarPath { get; set; }
    
    public ImageSource? Source
    {
        get => _source;
        set
        {
            _source = value;
            OnPropertyChanged(nameof(Source));
        }
    }

    public ICommand UpdateCurrentViewModelCommand { get; set; }
    
    public ICommand LogoutCommand { get; set; }

    public Navigator(
        IViewModelAbstractFactory viewModelAbstractFactory, 
        IAuthenticator authenticator)
    {
        Authenticator = authenticator;
        UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(this, viewModelAbstractFactory);
        LogoutCommand = new LogoutCommand(this, authenticator);
    }
    
    public void UpdateAvatar()
    {
        if (Authenticator.CurrentUser is { Avatar: not null })
        {
            LoadImage();
        }
    }
    
    private void LoadImage()
    {
        var bitmap = new BitmapImage();
            
        bitmap.BeginInit();
            
        bitmap.UriSource = new Uri(Authenticator.CurrentUser.Avatar!.ImageData, UriKind.Absolute);
        CurrentAvatarPath = Authenticator.CurrentUser.Avatar!.ImageData;
            
        bitmap.EndInit();
            
        Source = bitmap;
    }
}