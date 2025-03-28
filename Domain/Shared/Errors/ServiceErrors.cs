namespace Domain.Shared.Errors;

public static class ServiceErrors
{
    public static readonly Error FailedAuthenticateByEmail = new("Failed to sign in the system with incorrect email");
    public static readonly Error FailedAuthenticateByPassword = new("Failed to sign in the system with incorrect password");
    public static readonly Error FailedToRegisterUser = new("Failed to register user");
    public static readonly Error FailedToGetDeliveryInfoByDate = new("Failed to get delivery infos by date");
    public static readonly Error FailedToCreateUserDeliveryInfo = new("Failed to create user delivery infos");
    public static readonly Error FailedToGetAllCourierEmails = new("Failed to get all courier emails");
    public static readonly Error FailedToGetAllAllUserDeliveryInfoByCourier= new("Failed to get all user delivery infos by courier");
    public static readonly Error FailedToGetAllTrackingNumbersByUserEmail = new("Failed to get all tracking numbers by user email");
    public static readonly Error FailedToUpdateUser = new("Failed to update user info");
    public static readonly Error FailedToAddFillInFolder = new("Failed to add file into folder");
    public static readonly Error FailedToCreateProfileImage = new("Failed to create profile image");
    public static readonly Error FailedToUpdateUserAvatar = new("Failed to update user avatar");
    public static readonly Error NotFreeEmail = new("This email is already used in the system");
    public static readonly Error TrackingNumberContainsNonNumeric = new("Tracking number contains non numeric segments");
    public static readonly Error CourierNotFound = new("Courier Not Found");
    public static readonly Error RecipientNotFound = new("Courier Not Found");
    public static readonly Error CurrentUserNotFound = new("Current User Not Found");
    public static readonly Error NotAuthenticate = new("You are not authenticate, try to log in");
    public static readonly Error ExistsTrackingNumber = new("This tracking number is already existed");
    public static readonly Error NotCourier = new("This user is not courier");
    public static readonly Error NotPackages = new("Delivery info must contain at least one package");
}