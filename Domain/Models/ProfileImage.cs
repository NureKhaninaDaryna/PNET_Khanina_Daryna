namespace Domain.Models;

public class ProfileImage : BaseEntity
{
    public ProfileImage(string fileName, byte[] imageData)
    {
        FileName = fileName;
        ImageData = imageData;
    }

    public string FileName { get; private set; }
    
    public byte[] ImageData { get; private set; }

    public int UserId { get; set; }

    public virtual User User { get; set; }
}