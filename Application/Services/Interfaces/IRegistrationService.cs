using Domain.Enums;
using Domain.Shared.Results;

namespace Application.Services.Interfaces;

public interface IRegistrationService
{
    Task<ServiceResult> Register(string email, string password, string? userName, string? firstName, 
        string? lastName, string? phoneNumber, DateOnly? dayOfBirth, UserRole? role = UserRole.Recipient);
}