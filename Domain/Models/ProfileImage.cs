namespace Domain.Models;

public class ProfileImage : BaseEntity
{
    public ProfileImage(string name, string imageData)
    {
        Name = name;
        ImageData = imageData;
    }

    public string Name { get; private set; }
    
    public string ImageData { get; private set; }
}