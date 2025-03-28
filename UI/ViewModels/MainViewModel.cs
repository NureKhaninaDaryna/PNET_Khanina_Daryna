using UI.Enums;
using UI.State.Authenticators;
using UI.State.Navigators;

namespace UI.ViewModels;

public class MainViewModel : ViewModelBase
{
    public INavigator Navigator { get; set; }
    public IAuthenticator Authenticator { get; }

    public MainViewModel(INavigator navigator, IAuthenticator authenticator)
    {
        Navigator = navigator;
        Authenticator = authenticator;

        Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Login);
    }
}