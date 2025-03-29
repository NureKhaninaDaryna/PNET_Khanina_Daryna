using Domain.Enums;

namespace Domain.Models;

public class DeliveryStatusHistory : BaseEntity
{
    public int DeliveryInfoId { get; set; }

    public DeliveryStatus Status { get; set; }

    public DateTime ChangeDate { get; set; }

    public string AddressInProgress { get; set; } = null!;

    public virtual DeliveryInfo DeliveryInfo { get; set; } = null!;
}