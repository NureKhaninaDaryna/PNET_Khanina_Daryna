namespace Domain.Shared;

public static class FileLocationHelper
{
    public static string GetFileLocation(string projectName, string folderName)
    {
        var solutionRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;
        var projectRoot = Path.Combine(solutionRoot!, projectName);
        var folderPath = Path.Combine(projectRoot, folderName);
        
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        return folderPath;
    }
}