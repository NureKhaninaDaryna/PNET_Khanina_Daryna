using System.Collections.ObjectModel;
using System.Windows.Input;
using Application.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Enums;
using Domain.Models;

namespace DeliveryProject.MAUI.ViewModels;

public partial class UsersViewModel : ObservableObject
{
    private const string RolePreferenceKey = "SelectedUserRole";
    
    private readonly IUserService _userService;

    public UsersViewModel(IUserService userService)
    {
        _userService = userService;

        Users = new ObservableCollection<User>();
        Roles = new ObservableCollection<UserRole>(Enum.GetValues(typeof(UserRole)).Cast<UserRole>());

        LoadPreferences();
            
        FilterCommand = new AsyncRelayCommand(LoadUsersByRole);
        LoadAverageRatingCommand = new AsyncRelayCommand(LoadAverageRating);
        ResetAllFiltersCommand = new AsyncRelayCommand(LoadAllUsers);

        _ = LoadUsersByRole();
    }

    public ObservableCollection<User> Users { get; }
    public ObservableCollection<UserRole> Roles { get; }

    [ObservableProperty]
    private UserRole? selectedRole;

    [ObservableProperty]
    private double averageCourierRating;

    public ICommand FilterCommand { get; }
    public ICommand LoadAverageRatingCommand { get; }
    public ICommand ResetAllFiltersCommand { get; }

    private async Task LoadAllUsers()
    {
        var users = await _userService.GetAllUsers();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Users.Clear();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        });
    }

    private async Task LoadUsersByRole()
    {
        if (SelectedRole == null)
        {
            await LoadAllUsers();
            return;
        }

        OnSelectedRoleChanged(SelectedRole);
            
        var users = await _userService.GetUsersByRole(SelectedRole.Value);
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Users.Clear();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        });
    }

    private async Task LoadAverageRating()
    {
        AverageCourierRating = await _userService.GetAverageCourierRating();
    }

    private void LoadPreferences()
    {
        if (Preferences.ContainsKey(RolePreferenceKey))
        {
            var roleString = Preferences.Get(RolePreferenceKey, string.Empty);
            if (Enum.TryParse<UserRole>(roleString, out var role))
                SelectedRole = role;
        }
    }
    
    partial void OnSelectedRoleChanged(UserRole? value)
    {
        if (value != null)
        {
            Preferences.Set(RolePreferenceKey, value.ToString());
        }
        else
        {
            Preferences.Remove(RolePreferenceKey);
        }
    }
}