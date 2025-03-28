using Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace Domain.Models;

public class DeliveryInfo : BaseEntity
{
    public DeliveryInfo() { }

    public DeliveryInfo(DateOnly date)
    {
        Date = date;
    }
    
    public DeliveryInfo(User user, User courier, string trackingNumber)
    {
        RecipientId = user.Id;
        CourierId = courier.Id;
        Recipient = user;
        Courier = courier;
        TrackingNumber = trackingNumber;
    }
    
    public string TrackingNumber { get; set; }
    
    public DateOnly Date { get; init; }

    public int CourierId { get; set; }
    
    public User Courier { get; init; }
    
    public int SenderId { get; set; }
    
    public User Sender { get; init; }
    
    public int RecipientId  { get; set; }
    
    public User Recipient { get; init; }
    
    [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
    public IDictionary<DeliveryStatus, DateTime> DateStatus { get; set; } 
    
    public string StartAddress { get; set; }
    
    public string DeliveryAddress { get; set; }
}