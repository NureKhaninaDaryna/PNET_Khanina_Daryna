using Domain.Models;
using Domain.Shared.Results;

namespace Application.Services.Interfaces;

public interface IDeliveryInfoService
{
    Task<ServiceResult<List<DeliveryInfo>>> GetDeliveryInfosByDate(DateOnly dateOnly);
    
    Task<ServiceResult<List<DeliveryInfo>>> GetAllDeliveryInfos();
}