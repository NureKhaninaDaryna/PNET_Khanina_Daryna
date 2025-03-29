using Application.Services.Interfaces;
using Domain.Models;
using Domain.Shared.Errors;
using Domain.Shared.Results;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services.Implementations;

public class DeliveryInfoService : IDeliveryInfoService
{
    private readonly IRepository<DeliveryInfo> _deliveryInfoRepository;

    public DeliveryInfoService(IRepository<DeliveryInfo> deliveryInfoRepository)
    {
        _deliveryInfoRepository = deliveryInfoRepository;
    }
    
    public async Task<ServiceResult<List<DeliveryInfo>>> GetDeliveryInfosByDate(DateOnly dateOnly)
    {
        try
        {
            return ServiceResult<List<DeliveryInfo>>.Success([]);
        }
        catch 
        {
            return ServiceResult<List<DeliveryInfo>>.Failure(ServiceErrors.FailedToGetDeliveryInfoByDate);
        }
    }

    public async Task<ServiceResult<List<DeliveryInfo>>> GetAllDeliveryInfos()
    {
        try
        {
            var deliveryInfos = await _deliveryInfoRepository.GetAllAsync();

            return ServiceResult<List<DeliveryInfo>>.Success(deliveryInfos);
        }
        catch 
        {
            return ServiceResult<List<DeliveryInfo>>.Failure(ServiceErrors.FailedToGetDeliveryInfoByDate);
        }
    }
}