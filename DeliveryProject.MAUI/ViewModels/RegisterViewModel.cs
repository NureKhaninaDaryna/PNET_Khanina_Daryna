using System.Windows.Input;
using Application.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Enums;

namespace DeliveryProject.MAUI.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly IRegistrationService _authenticator;
        
        public DateTime MinimumBirthDate => DateTime.Today.AddYears(-65);
        public DateTime MaximumBirthDate => DateTime.Today.AddYears(-18);
        
        [ObservableProperty] private string email = string.Empty;
        [ObservableProperty] private string password = string.Empty;
        [ObservableProperty] private string confirmPassword = string.Empty;
        [ObservableProperty] private UserRole role;
        [ObservableProperty] private string? username;
        [ObservableProperty] private string? firstName;
        [ObservableProperty] private string? lastName;
        [ObservableProperty] private string? phoneNumber;
        [ObservableProperty] private DateOnly? dayOfBirth;
        
        [ObservableProperty]
        private string feedbackMessage;

        public IEnumerable<UserRole> UserRoles => Enum.GetValues(typeof(UserRole)).Cast<UserRole>();

        public ICommand RegisterCommand { get; }

        public RegisterViewModel(IRegistrationService authenticator)
        {
            _authenticator = authenticator;
            RegisterCommand = new AsyncRelayCommand(OnRegister);
        }

        private async Task OnRegister()
        {
            if (string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword) ||
                string.IsNullOrWhiteSpace(Username))
            {
                FeedbackMessage = "Please fill in all required fields.";
                return;
            }

            if (Password != ConfirmPassword)
            {
                FeedbackMessage = "Passwords do not match.";
                return;
            }
            
            var result = await _authenticator.Register(Email, Password, Username, FirstName, LastName, PhoneNumber, DayOfBirth, Role);
            if (result.IsSuccess)
            {
                FeedbackMessage = "Registration successful!";
            }
            else
            {
                FeedbackMessage = $"Error: {result.Error.Message}";
            }
        }
    }
}
