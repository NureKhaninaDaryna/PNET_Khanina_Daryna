using Application.Dto;
using Domain.Models;
using Domain.Shared.Results;

namespace Application.Services.Interfaces;

public interface IUserDeliveryInfoService
{
    Task<ServiceResult<DeliveryInfo>> CreateUserDeliveryInfo(
        string courierEmail, string recipientEmail, string startAddress, 
        string deliveryAddress, User? currentUser, List<Package> packages);

    Task<ServiceResult<List<string>>> GetAllCourierEmails();
    
    Task<ServiceResult<List<string>>> GetAllRecipientEmails();
    
    Task<ServiceResult<List<string>>> GetAllTrackingNumbersByUserEmail(string currentUserEmail);

    ServiceResult<List<DeliveryInfo>> GetAllUserDeliveryInfoByCourier(string courierEmail);
}