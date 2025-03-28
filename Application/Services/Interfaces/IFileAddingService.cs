using Domain.Shared.Results;

namespace Application.Services.Interfaces;

public interface IFileAddingService
{
    ServiceResult<string> AddFileToFolder(string filePath, string projectName, string folderName);
}