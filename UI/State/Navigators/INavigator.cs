using System.Windows.Input;
using UI.ViewModels;

namespace UI.State.Navigators;

public interface INavigator
{
    ViewModelBase CurrentViewModel { get; set; }
    
    ICommand UpdateCurrentViewModelCommand { get; }

    void UpdateAvatar();
}