using Domain.Shared.Results;

namespace Application.Services.Interfaces;

public interface IProfileImageService
{
    Task<ServiceResult<Uri>> CreateImage(string filePath, int userId);
}