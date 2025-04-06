using Domain.Enums;
using Domain.Models;

namespace DeliveryProject.MAUI.State.Authenticators;

public interface IAuthenticator
{
    User? CurrentUser { get; }
    
    bool IsLoggedIn { get; }
    
    UserRole? Role { get; }
    
    Task<(bool, string?)> SignIn(string email, string password);
    
    Task<(bool, string?)> Register(string email, string password, string? userName, string? firstName, 
        string? lastName, string? phoneNumber, DateOnly? dayOfBirth, UserRole? role = UserRole.Recipient);

    void LogOut();
}