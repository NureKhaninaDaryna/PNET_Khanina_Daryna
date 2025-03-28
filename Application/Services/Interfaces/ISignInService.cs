using Application.Dto;
using Domain.Models;
using Domain.Shared.Results;

namespace Application.Services.Interfaces;

public interface ISignInService
{
    Task<ServiceResult<User>> Authenticate(string email, string password);
}