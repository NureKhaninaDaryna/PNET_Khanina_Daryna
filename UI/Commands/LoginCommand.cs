using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using UI.Enums;
using UI.State.Authenticators;
using UI.State.Navigators;
using UI.ViewModels;

namespace UI.Commands;

public class LoginCommand : ICommand
{
    private readonly IAuthenticator _authenticator;
    private readonly LoginViewModel _loginViewModel;
    private readonly IRenavigator _homeRenavigator;
    private readonly INavigator _navigator;

    public LoginCommand(
        LoginViewModel loginViewModel, 
        IAuthenticator authenticator, 
        IRenavigator homeRenavigator,
        INavigator navigator)
    {
        _authenticator = authenticator;
        _loginViewModel = loginViewModel;
        _homeRenavigator = homeRenavigator;
        _navigator = navigator;
    }
    
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public async void Execute(object? parameter)
    {
        var result = await _authenticator.SignIn(_loginViewModel.Username, parameter.ToString());

        _navigator.UpdateAvatar();
        _homeRenavigator.Renavigate();
        
        MessageBox.Show(result.Item1 == false ? result.Item2 : "You successfully log in");
    }

    public event EventHandler? CanExecuteChanged;
}