namespace Domain.Models;

public class Package : BaseEntity
{
    public int DeliveryInfoId { get; set; }
    
    public DeliveryInfo DeliveryInfo { get; set; } = null!;

    public double Weight { get; set; }
    
    public string Dimensions { get; set; } = "10/10";
    
    public string Content { get; set; } = string.Empty;
    
    public bool Fragile { get; set; } 
    
    public double Price { get; set; } 
}