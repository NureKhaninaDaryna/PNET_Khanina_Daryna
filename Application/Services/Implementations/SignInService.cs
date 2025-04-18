﻿using Application.Dto;
using Application.Services.Interfaces;
using Domain.Models;
using Domain.Shared.Errors;
using Domain.Shared.Results;

namespace Application.Services.Implementations;

public class SignInService : ISignInService
{
    private readonly IUserService _userService;
    private readonly IPasswordHashing _passwordHashing;
    private readonly IJwtService _jwtService;

    public SignInService(
        IUserService userService, 
        IPasswordHashing passwordHashing,
        IJwtService jwtService)
    {
        _userService = userService;
        _passwordHashing = passwordHashing;
        _jwtService = jwtService;
    }
    
    public async Task<ServiceResult<AuthUserDto>> Authenticate(string email, string password)
    {
        var user = await _userService.GetUserByEmail(email);

        if (user == null)
        {
            return ServiceResult<AuthUserDto>.Failure(ServiceErrors.FailedAuthenticateByEmail);
        }

        var verifyPasswordResult = _passwordHashing.VerifyPassword(password, user.PasswordHash);

        if (!verifyPasswordResult)
        {
            return ServiceResult<AuthUserDto>.Failure(ServiceErrors.FailedAuthenticateByPassword);
        }
        
        var token = _jwtService.GenerateJwtToken(user);

        return ServiceResult<AuthUserDto>.Success(new AuthUserDto { User = user, Token = token });
    }
}

public class AuthUserDto
{
    public User User { get; set; } = null!;

    public string Token { get; set; } = null!;
}