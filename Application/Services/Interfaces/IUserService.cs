using Domain.Enums;
using Domain.Models;
using Domain.Shared.Results;

namespace Application.Services.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByEmail(string email);
    
    Task<List<User>> GetUsersByRole(UserRole role);
    
    Task<List<User>> GetAllUsers();
    
    Task<double> GetAverageCourierRating();
    
    Task<User?> GetUserById(int id);
    
    Task<bool> IsFreeEmail(string email);

    Task<ServiceResult<User>> UpdateUser(User updatedUser, string currentEmail);
    
    Task<ServiceResult> UpdateUserAvatar(ProfileImage profileImage, int userId);
}