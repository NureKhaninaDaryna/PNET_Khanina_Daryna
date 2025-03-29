using Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace Domain.Models;

public class DeliveryInfo : BaseEntity
{
    public string TrackingNumber { get; set; } = null!;
    
    public int CourierId { get; set; }
    
    public int RecipientId  { get; set; }
    
    public string StartAddress { get; set; } = null!;
    
    public string DeliveryAddress { get; set; } = null!;
    
    public virtual User Recipient { get; init; } = null!;

    public virtual User Courier { get; init; } = null!;

    public virtual IEnumerable<DeliveryStatusHistory> DeliveryStatusHistories { get; set; } = new List<DeliveryStatusHistory>();
    
    public virtual IEnumerable<Package> Packages { get; set; } = new List<Package>();
}