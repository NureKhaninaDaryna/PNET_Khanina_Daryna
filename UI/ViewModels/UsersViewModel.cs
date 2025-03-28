using System.Collections.ObjectModel;
using System.Windows.Input;
using Application.Services.Interfaces;
using Domain.Enums;
using Domain.Models;

namespace UI.ViewModels;

public class UsersViewModel : ViewModelBase
{
    private readonly IUserService _userService;
    
    public UsersViewModel(IUserService userService)
    {
        _userService = userService;
        Users = new ObservableCollection<User>();
        Roles = new ObservableCollection<UserRole>(Enum.GetValues(typeof(UserRole)).Cast<UserRole>());
        FilterCommand = new RelayCommand(async () => await LoadUsersByRole());
        LoadAverageRatingCommand = new RelayCommand(async () => await LoadAverageRating());
        ResetAllFiltersCommand = new RelayCommand(async () => await LoadAllUsers());
        
        _ = LoadAllUsers();
    }

    public ObservableCollection<User> Users { get; set; }
    public ObservableCollection<UserRole> Roles { get; set; }
    
    private UserRole? _selectedRole;
    public UserRole? SelectedRole
    {
        get => _selectedRole;
        set { _selectedRole = value; OnPropertyChanged(nameof(SelectedRole)); }
    }

    private double _averageRating = 0;
    public double AverageCourierRating
    {
        get => _averageRating;
        set { _averageRating = value; OnPropertyChanged(nameof(AverageCourierRating)); }
    }
    
    public ICommand FilterCommand { get; }
    public ICommand LoadAverageRatingCommand { get; }
    public ICommand ResetAllFiltersCommand { get; }

    private async Task LoadAllUsers()
    {
        var users = await _userService.GetAllUsers();
        Users.Clear();
        foreach (var user in users)
        {
            Users.Add(user);
        }
    }

    private async Task LoadUsersByRole()
    {
        if (SelectedRole == null) return;

        var users = await _userService.GetUsersByRole(SelectedRole.Value);
        Users.Clear();
        foreach (var user in users)
        {
            Users.Add(user);
        }
    }
    
    private async Task LoadAverageRating()
    {
        AverageCourierRating = await _userService.GetAverageCourierRating();
    }
}