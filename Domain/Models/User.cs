using Domain.Enums;

namespace Domain.Models;

public class User : BaseEntity
{
    public User() {}
    
    public User(string email, string passwordHash, UserRole role = UserRole.Recipient)
    {
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }

    public User(string email, string passwordHash, string? userName, string? firstName, string? lastName, string? phoneNumber, DateOnly? dayOfBirth, UserRole? role = UserRole.Recipient)
    {
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        DayOfBirth = dayOfBirth;
        Avatar = default;
        
        if (role == UserRole.Recipient)
            Rating = 0.0;
    }

    public string Email { get; set; }
    
    public string PasswordHash { get; set; }
    
    public UserRole? Role { get; private set; }
    
    public string? UserName { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? PhoneNumber { get; set; }

    public DateOnly? DayOfBirth { get; set; }
    
    public ProfileImage? Avatar { get; set; }
    
    public double? Rating { get; set; }

    public void UpdateRating(double rate)
    {
        if (Role == UserRole.Recipient) return;

        Rating = rate;
    }
}