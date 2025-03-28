using System.Windows.Input;
using UI.Commands;
using UI.State.Authenticators;
using UI.State.Navigators;

namespace UI.ViewModels;

public class LoginViewModel : ViewModelBase
{
    public IAuthenticator Authenticator { get; }
    
    private string _username;

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    public ICommand LoginCommand { get; }

    public LoginViewModel(IAuthenticator authenticator, IRenavigator homeRenavigator, INavigator navigator)
    {
        Authenticator = authenticator;
        LoginCommand = new LoginCommand(this, authenticator, homeRenavigator, navigator);
    }
}