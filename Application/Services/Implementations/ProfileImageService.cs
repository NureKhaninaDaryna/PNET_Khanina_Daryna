using Application.Services.Interfaces;
using Domain.Models;
using Domain.Shared.Errors;
using Domain.Shared.Results;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services.Implementations;

public class ProfileImageService : IProfileImageService
{
    private readonly string _projectName = "UI";
    private readonly string _folderName = "Assets";
    
    private readonly IRepository<ProfileImage> _profileImageRepository;
    private readonly IFileAddingService _fileAddingService;
    private readonly IUserService _userService;

    public ProfileImageService(
        IRepository<ProfileImage> profileImageRepository, 
        IFileAddingService fileAddingService,
        IUserService userService)
    {
        _profileImageRepository = profileImageRepository;
        _fileAddingService = fileAddingService;
        _userService = userService;
    }
    
    public async Task<ServiceResult<Uri>> CreateImage(string filePath, int userId)
    {
        var targetFileResult = _fileAddingService.AddFileToFolder(filePath, _projectName, _folderName);

        if (!targetFileResult.IsSuccess) return ServiceResult<Uri>.Failure(targetFileResult.Error);
        
        try
        {
            var profileImage = new ProfileImage("userAvatar", targetFileResult.Value);

            await _profileImageRepository.CreateAsync(profileImage);

            var updateAvatarResult = await _userService.UpdateUserAvatar(profileImage, userId);
           
           return  ServiceResult<Uri>.Success(new Uri(targetFileResult.Value, UriKind.Absolute));
        }
        catch 
        {
            return ServiceResult<Uri>.Failure(ServiceErrors.FailedToCreateProfileImage);
        }
    }
}