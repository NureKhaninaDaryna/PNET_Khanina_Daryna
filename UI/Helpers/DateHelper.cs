namespace UI.Helpers;

public class DateHelper
{
    public DateTime MinDateOfBirth { get; }
    
    public DateTime MaxDateOfBirth { get; }
    
    public DateHelper()
    {
        MaxDateOfBirth = DateTime.Today.AddYears(-16);
        MinDateOfBirth = DateTime.Today.AddYears(-100);
    }
}