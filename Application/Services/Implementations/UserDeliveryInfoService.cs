using System.Security.Cryptography;
using Application.Dto;
using Application.Services.Interfaces;
using Domain.Enums;
using Domain.Models;
using Domain.Shared.Errors;
using Domain.Shared.Results;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services.Implementations;

public class UserDeliveryInfoService : IUserDeliveryInfoService
{
    private readonly IRepository<DeliveryInfo> _userDeliveryInfoRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Package> _packageRepository;

    private const int TrackingNumberLength = 12;
    
    public UserDeliveryInfoService(
        IRepository<DeliveryInfo> userDeliveryInfoRepository, 
        IRepository<User> userRepository,
        IRepository<Package> packageRepository)
    {
        _userDeliveryInfoRepository = userDeliveryInfoRepository;
        _userRepository = userRepository;
        _packageRepository = packageRepository;
    }
    
    public async Task<ServiceResult<DeliveryInfo>> CreateUserDeliveryInfo(
        string courierEmail, string recipientEmail, string startAddress, 
        string deliveryAddress, User? userResponse, List<Package> packages)
    {
        if (!packages.Any())
        {
            return ServiceResult<DeliveryInfo>.Failure(ServiceErrors.NotPackages);
        }
        
        if (userResponse == null)
        {
            return ServiceResult<DeliveryInfo>.Failure(ServiceErrors.NotAuthenticate);
        }
        
        var courier = (await _userRepository.GetByPredicateAsync(u => u.Email == courierEmail)).FirstOrDefault();
        if (courier == null)
        {
            return ServiceResult<DeliveryInfo>.Failure(ServiceErrors.CourierNotFound);
        }
        
        var recipient = (await _userRepository.GetByPredicateAsync(u => u.Email == recipientEmail)).FirstOrDefault();
        if (recipient == null)
        {
            return ServiceResult<DeliveryInfo>.Failure(ServiceErrors.RecipientNotFound);
        }
        
        var currentUser = (await _userRepository.GetByPredicateAsync(u => u.Email == userResponse.Email)).FirstOrDefault();
        if (currentUser == null)
        {
            return ServiceResult<DeliveryInfo>.Failure(ServiceErrors.CurrentUserNotFound);
        }

        var trackingNumber = GenerateTrackingNumber();
        
        if (!await IsValidTrackingNumber(trackingNumber, currentUser.Id))
        {
            return ServiceResult<DeliveryInfo>.Failure(ServiceErrors.ExistsTrackingNumber);
        }

        var userDeliveryInfo = new DeliveryInfo
        {
            TrackingNumber = trackingNumber,
            Date = DateOnly.FromDateTime(DateTime.Now),
            DeliveryAddress = deliveryAddress,
            StartAddress = startAddress,
            RecipientId = recipient.Id,
            CourierId = courier.Id,
            Recipient = recipient,
            Courier = courier,
            SenderId = currentUser.Id,
            Sender = currentUser,
            DateStatus = new Dictionary<DeliveryStatus, DateTime>
            {
                { DeliveryStatus.Pending, DateTime.Now }
            }
        };
        
        try
        {
            await _userDeliveryInfoRepository.CreateAsync(userDeliveryInfo);

            foreach (var package in packages)
            {
                var packageToCreate = new Package
                {
                    DeliveryInfoId = await _userDeliveryInfoRepository.GetLastIdAsync(),
                    DeliveryInfo = userDeliveryInfo,
                    Weight = package.Weight,
                    Dimensions = package.Dimensions,
                    Content = package.Content,
                    Fragile = package.Fragile,
                    Price = package.Price,
                };
                
                await _packageRepository.CreateAsync(packageToCreate);
            }
            
            return ServiceResult<DeliveryInfo>.Success(userDeliveryInfo);
        }
        catch
        {
            return ServiceResult<DeliveryInfo>.Failure(ServiceErrors.FailedToCreateUserDeliveryInfo);
        }
    }

    public async Task<ServiceResult<List<string>>> GetAllCourierEmails()
    {
        try
        {
            var emails = (await _userRepository.GetByPredicateAsync(u => u.Role == UserRole.Courier)).Select(u => u.Email).ToList();
            
            return ServiceResult<List<string>>.Success(emails);
        }
        catch 
        {
            return ServiceResult<List<string>>.Failure(ServiceErrors.FailedToGetAllCourierEmails);
        }
    }

    public async Task<ServiceResult<List<string>>> GetAllRecipientEmails()
    {
        try
        {
            var emails = (await _userRepository.GetByPredicateAsync(u => u.Role == UserRole.Recipient)).Select(u => u.Email).ToList();
            
            return ServiceResult<List<string>>.Success(emails);
        }
        catch 
        {
            return ServiceResult<List<string>>.Failure(ServiceErrors.FailedToGetAllCourierEmails);
        }
    }

    public async Task<ServiceResult<List<string>>> GetAllTrackingNumbersByUserEmail(string currentUserEmail)
    {
        try
        {
            var currentUser = (await _userRepository.GetByPredicateAsync(u => u.Email == currentUserEmail)).FirstOrDefault();
        
            if (currentUser == null)
            {
                return ServiceResult<List<string>>.Failure(ServiceErrors.CurrentUserNotFound);
            }
            
            var trackingNumbers = (await _userDeliveryInfoRepository
                    .GetByPredicateAsync(udi => udi.RecipientId == currentUser.Id))
                    .Select(udi => udi.TrackingNumber)
                    .ToList();
            
            return ServiceResult<List<string>>.Success(trackingNumbers);
        }
        catch 
        {
            return ServiceResult<List<string>>.Failure(ServiceErrors.FailedToGetAllTrackingNumbersByUserEmail);
        }
    }

    public ServiceResult<List<DeliveryInfo>> GetAllUserDeliveryInfoByCourier(string courierEmail)
    {
        var courier = _userRepository.GetByPredicate(u => u.Email == courierEmail).FirstOrDefault();
        
        if (courier == null)
        {
            return ServiceResult<List<DeliveryInfo>>.Failure(ServiceErrors.CourierNotFound);
        }

        if (courier.Role != UserRole.Courier)
        {
            return ServiceResult<List<DeliveryInfo>>.Failure(ServiceErrors.NotCourier);
        }
        
        try
        {
            var userDeliveryInfos = _userDeliveryInfoRepository.GetByPredicate(udi => udi.CourierId == courier.Id);
            
            return ServiceResult<List<DeliveryInfo>>.Success(userDeliveryInfos);
        }
        catch (Exception e)
        {
            return ServiceResult<List<DeliveryInfo>>.Failure(ServiceErrors.FailedToGetAllAllUserDeliveryInfoByCourier);
        }
    }

    private bool IsNumeric(string value)
    {
        return int.TryParse(value, out _);
    }

    private async Task<bool> IsValidTrackingNumber(string trackingNumber, int userId)
    {
        return (await _userDeliveryInfoRepository.GetByPredicateAsync(udi =>
            udi.RecipientId == userId && udi.TrackingNumber == trackingNumber)).Count == 0;
    }

    public static string GenerateTrackingNumber()
    {
        return new string(Enumerable.Range(0, TrackingNumberLength)
            .Select(_ => (char)('0' + RandomNumberGenerator.GetInt32(0, 10)))
            .ToArray());
    }
}