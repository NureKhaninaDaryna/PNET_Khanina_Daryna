using Domain.Enums;
using Domain.Models;

namespace Application.Dto;

public class AuthenticateResponse
{
    public AuthenticateResponse() {}
    
    public AuthenticateResponse(User user, string token)
    {
        Id = user.Id;
        Email = user.Email;
        Token = token;
        Role = user.Role;
    }
    
    public int Id { get; set; }
    
    public string Email { get; set; }
    
    public string Token { get; set; }
    
    public UserRole? Role { get; set; }
}