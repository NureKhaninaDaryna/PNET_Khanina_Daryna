using UI.Enums;

namespace UI.ViewModels.Factories;

public class ViewModelAbstractFactory(
    CreateViewModel<HomeViewModel> createHomeViewModel,
    CreateViewModel<LoginViewModel> createLoginViewModel,
    CreateViewModel<RegisterViewModel> createRegisterViewModel,
    CreateViewModel<DeliveryFormViewModel> createDeliveryFormViewModel,
    CreateViewModel<CourierHomeViewModel> createCourierHomeViewModel,
    CreateViewModel<UserProfileViewModel> createUserProfileViewModel,
    CreateViewModel<UsersViewModel> createUsersViewModel)
    : IViewModelAbstractFactory
{
    public ViewModelBase CreateViewModel(ViewType viewType)
    {
        return viewType switch
        {
            ViewType.Home => createHomeViewModel(),
            ViewType.Login => createLoginViewModel(),
            ViewType.Register => createRegisterViewModel(),
            ViewType.DeliveryForm => createDeliveryFormViewModel(),
            ViewType.CourierHome => createCourierHomeViewModel(),
            ViewType.UserProfile => createUserProfileViewModel(),
            ViewType.Users => createUsersViewModel(),
            _ => throw new ArgumentException("The ViewType does not have a ViewModel", nameof(viewType))
        };
    }
}