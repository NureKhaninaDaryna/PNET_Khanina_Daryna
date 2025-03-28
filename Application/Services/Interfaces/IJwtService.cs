using Domain.Models;

namespace Application.Services.Interfaces;

public interface IJwtService
{
    public string GenerateJwtToken(User user);
    
    public Guid? ValidateJwtToken(string? token);
}