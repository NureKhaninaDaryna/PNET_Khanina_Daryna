using System.Windows.Forms;
using System.Windows.Input;
using UI.State.Authenticators;
using UI.State.Navigators;
using UI.ViewModels;

namespace UI.Commands;

public class RegisterCommand : ICommand
{
    private readonly IAuthenticator _authenticator;
    private readonly IRenavigator _registerRenavigator;
    private readonly RegisterViewModel _registerViewModel;

    public RegisterCommand(RegisterViewModel registerViewModel, IAuthenticator authenticator, IRenavigator registerRenavigator)
    {
        _authenticator = authenticator;
        _registerRenavigator = registerRenavigator;
        _registerViewModel = registerViewModel;
    }
    
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public async void Execute(object? parameter)
    {
        var result = await _authenticator.Register(
            _registerViewModel.Email, parameter.ToString(), _registerViewModel.Username, _registerViewModel.FirstName, _registerViewModel.LastName, 
            _registerViewModel.PhoneNumber, _registerViewModel.DayOfBirth, _registerViewModel.Role);
        
        MessageBox.Show(result.Item1 == false ? result.Item2 : "You successfully log in");
        
        _registerRenavigator.Renavigate();
    }

    public event EventHandler? CanExecuteChanged;
}