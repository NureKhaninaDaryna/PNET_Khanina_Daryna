using Application.Services.Interfaces;
using Domain.Shared;
using Domain.Shared.Errors;
using Domain.Shared.Results;

namespace Application.Services.Implementations;

public class FileAddingService : IFileAddingService
{
    public ServiceResult<string> AddFileToFolder(string filePath, string projectName, string folderName)
    {
        try
        {
            var fileName = Path.GetFileName(filePath);
        
            var targetPath = Path.Combine(FileLocationHelper.GetFileLocation(projectName, folderName), fileName);

            File.Copy(filePath, targetPath, overwrite: true);

            return ServiceResult<string>.Success(targetPath);
        }
        catch
        {
            return ServiceResult<string>.Failure(ServiceErrors.FailedToAddFillInFolder);
        }
    }
}