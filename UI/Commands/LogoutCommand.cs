using System.Windows.Input;
using UI.Enums;
using UI.State.Authenticators;
using UI.State.Navigators;

namespace UI.Commands;

public class LogoutCommand : ICommand
{
    private readonly Navigator _navigator;
    private readonly IAuthenticator _authenticator;

    public LogoutCommand(Navigator navigator, IAuthenticator authenticator)
    {
        _navigator = navigator;
        _authenticator = authenticator;
    }
    
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _authenticator.LogOut();
        _navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Login);
    }

    public event EventHandler? CanExecuteChanged;
}