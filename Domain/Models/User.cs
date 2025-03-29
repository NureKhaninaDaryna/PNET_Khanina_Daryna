using Domain.Enums;

namespace Domain.Models;

public class User : BaseEntity
{
    public User() { }
    
    public User(string email, string passwordHash, string? userName, string? firstName, string? lastName, string? phoneNumber, DateOnly? dayOfBirth, UserRole? role = UserRole.Recipient)
    {
        Email = email;
        PasswordHash = passwordHash;
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
    
    public UserRole Role { get; set; }
    
    public string UserName { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string PhoneNumber { get; set; }

    public DateOnly? DayOfBirth { get; set; }
    
    public double Rating { get; set; }

    public int  AvatarId { get; set; }
    
    public virtual ProfileImage? Avatar { get; set; }

    public virtual IEnumerable<DeliveryInfo> CourierDeliveryInfos { get; set; }
    
    public virtual IEnumerable<DeliveryInfo> DeliveryInfos { get; set; }

    public void UpdateRating(double rate)
    {
        if (Role != UserRole.Courier) return;

        Rating = rate;
    }
}