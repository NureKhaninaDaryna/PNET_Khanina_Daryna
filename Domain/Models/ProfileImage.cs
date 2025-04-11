namespace Domain.Models;

public class ProfileImage : BaseEntity
{
    public ProfileImage(string fileName, string imageData)
    {
        FileName = fileName;
        ImageData = imageData;
    }

    public string FileName { get; private set; }
    
    public string ImageData { get; private set; }

    public int UserId { get; set; }

    public virtual User User { get; set; }
}