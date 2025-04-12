using Application.Services.Interfaces;
using Domain.Enums;
using Domain.Models;
using Domain.Shared.Errors;
using Domain.Shared.Results;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services.Implementations;

public class UserService : IUserService
{
    private readonly IRepository<User> _repository;

    public UserService(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<User?> GetUserByEmail(string email) =>
        (await _repository.GetByPredicateAsync(u => u.Email == email)).FirstOrDefault();

    public async Task<List<User>> GetUsersByRole(UserRole role) =>
        (await _repository.GetByPredicateAsync(u => u.Role == role));

    public async Task<List<User>> GetAllUsers() => await _repository.GetAllAsync();

    public async Task<double> GetAverageCourierRating() => 0;

    public async Task<User?> GetUserById(int id) => await _repository.GetByIdAsync(id);

    public async Task<bool> IsFreeEmail(string email) => await GetUserByEmail(email) == null;

    public async Task<ServiceResult<User>> UpdateUser(User updatedUser, string currentEmail)
    {
        try
        {
            if (currentEmail != updatedUser.Email && !await IsFreeEmail(updatedUser.Email))
            {
                return ServiceResult<User>.Failure(ServiceErrors.NotFreeEmail);
            }

            await _repository.UpdateAsync(updatedUser);

            return ServiceResult<User>.Success(updatedUser);
        }
        catch (Exception ex)
        {
            return ServiceResult<User>.Failure(ServiceErrors.FailedToUpdateUser);
        }
    }

    public async Task<ServiceResult> UpdateUserAvatar(ProfileImage profileImage, int userId)
    {
        try
        {
            var user = await _repository.GetByIdAsync(userId);
            
            if (user == null)
            {
                return ServiceResult<User>.Failure(ServiceErrors.CurrentUserNotFound);
            }

            user.Avatar = profileImage;

            await _repository.UpdateAsync(user);

            return ServiceResult.Success;
        }
        catch 
        {
            return ServiceResult.Failure(ServiceErrors.FailedToUpdateUserAvatar);
        }
    }
}
    