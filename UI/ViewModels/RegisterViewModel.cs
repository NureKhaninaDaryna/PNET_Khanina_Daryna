using System.Windows.Input;
using Domain.Enums;
using UI.Commands;
using UI.Helpers;
using UI.State.Authenticators;
using UI.State.Navigators;

namespace UI.ViewModels;

public class RegisterViewModel : ViewModelBase
{
    private string _email;
    private UserRole _role;
    private string? _username;
    private string? _firstName;
    private string? _lastName;
    private string? _phoneNumber;
    private DateOnly? _dayOfBirth;
    
    public IAuthenticator Authenticator { get; }

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }
    
    public UserRole Role
    {
        get => _role;
        set
        {
            _role = value;
            OnPropertyChanged(nameof(Role));
        }
    }
    
    public string? Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }
    
    public string? FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }
    
    public string? LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }
    
    public string? PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            _phoneNumber = value;
            OnPropertyChanged(nameof(PhoneNumber));
        }
    }
    
    public DateOnly? DayOfBirth
    {
        get => _dayOfBirth;
        set
        {
            _dayOfBirth = value;
            OnPropertyChanged(nameof(DayOfBirth));
        }
    }

    public DateHelper DateHelper { get; set; }
    
    public IEnumerable<UserRole> UserRoles => Enum.GetValues(typeof(UserRole)).Cast<UserRole>();
    
    public ICommand RegisterCommand { get; }

    public RegisterViewModel(IAuthenticator authenticator, IRenavigator registerRenavigator)
    {
        Authenticator = authenticator;
        RegisterCommand = new RegisterCommand(this, authenticator, registerRenavigator);
        DateHelper = new DateHelper();
    }
}