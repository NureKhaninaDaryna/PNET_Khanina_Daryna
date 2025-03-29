using Application.Dto;
using Application.Services.Implementations;
using Domain.Models;
using Domain.Shared.Results;

namespace Application.Services.Interfaces;

public interface ISignInService
{
    Task<ServiceResult<AuthUserDto>> Authenticate(string email, string password);
}