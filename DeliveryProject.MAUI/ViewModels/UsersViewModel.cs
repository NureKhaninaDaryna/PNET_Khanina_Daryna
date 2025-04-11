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
    private readonly IUserService _userService;

    public UsersViewModel(IUserService userService)
    {
        _userService = userService;

        Users = new ObservableCollection<User>();
        Roles = new ObservableCollection<UserRole>(Enum.GetValues(typeof(UserRole)).Cast<UserRole>());
        
        FilterCommand = new AsyncRelayCommand(LoadUsersByRole);
        LoadAverageRatingCommand = new AsyncRelayCommand(LoadAverageRating);
        ResetAllFiltersCommand = new AsyncRelayCommand(LoadAllUsers);

        _ = LoadAllUsers();
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
        if (SelectedRole == null) return;

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
}