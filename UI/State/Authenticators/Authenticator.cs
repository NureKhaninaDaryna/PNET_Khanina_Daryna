using Application.Dto;
using Application.Services.Interfaces;
using Domain.Enums;
using Domain.Models;
using Domain.Shared.Results;
using UI.Models;

namespace UI.State.Authenticators;

public class Authenticator : ObservableObject, IAuthenticator
{
    private readonly ISignInService _signInService;
    private readonly IRegistrationService _registrationService;

    private User? _currentUser;

    public User? CurrentUser
    {
        get => _currentUser;
        private set
        {
            _currentUser = value;
            OnPropertyChanged(nameof(CurrentUser));
            OnPropertyChanged(nameof(IsLoggedIn));
            OnPropertyChanged(nameof(Role));
        }
    }

    public bool IsLoggedIn => CurrentUser != null;
    
    public UserRole? Role => CurrentUser != null ? CurrentUser.Role : null;

    public Authenticator(ISignInService signInService, IRegistrationService registrationService)
    {
        _signInService = signInService;
        _registrationService = registrationService;
    }
    
    public async Task<(bool, string?)> SignIn(string email, string password)
    {
        var result = await _signInService.Authenticate(email, password);

        if (!result.IsSuccess) return (result.IsSuccess, result.Error.Message);
        
        CurrentUser = result.Value.User;

        return (result.IsSuccess, null);
    }

    public async Task<(bool, string?)> Register(string email, string password, string? userName, string? firstName, 
        string? lastName, string? phoneNumber, DateOnly? dayOfBirth, UserRole? role = UserRole.Recipient)
    {
        var result = await _registrationService.Register(email, password, userName, firstName, lastName, phoneNumber, dayOfBirth, role);
        
        return !result.IsSuccess ? (result.IsSuccess, result.Error.Message) : (result.IsSuccess, null);
    }

    public void LogOut()
    {
        CurrentUser = default;
    }
}