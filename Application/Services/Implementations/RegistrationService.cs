using Application.Services.Interfaces;
using Domain.Enums;
using Domain.Models;
using Domain.Shared.Errors;
using Domain.Shared.Results;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services.Implementations;

public class RegistrationService : IRegistrationService
{
    private readonly IUserService _userService;
    private readonly IPasswordHashing _passwordHashing;
    private readonly IRepository<User> _repository;

    public RegistrationService(
        IUserService userService, 
        IPasswordHashing passwordHashing, 
        IRepository<User> repository)
    {
        _userService = userService;
        _passwordHashing = passwordHashing;
        _repository = repository;
    }

    public async Task<ServiceResult> Register(string email, string password, string? userName, string? firstName, 
        string? lastName, string? phoneNumber, DateOnly? dayOfBirth, UserRole? role = UserRole.Recipient)
    {
        if (!await _userService.IsFreeEmail(email))
        {
            return ServiceResult.Failure(ServiceErrors.NotFreeEmail);
        }

        var passwordHash = _passwordHashing.HashPassword(password);

        var user = new User(email, passwordHash, userName, firstName, lastName, phoneNumber, dayOfBirth, role);

        try
        {
            await _repository.CreateAsync(user);

            return ServiceResult.Success;
        }
        catch 
        {
            return ServiceResult.Failure(ServiceErrors.FailedToRegisterUser);
        }
    }
}