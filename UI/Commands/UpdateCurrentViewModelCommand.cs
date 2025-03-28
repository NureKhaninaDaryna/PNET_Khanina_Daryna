using System.Windows.Input;
using UI.Enums;
using UI.State.Navigators;
using UI.ViewModels;
using UI.ViewModels.Factories;

namespace UI.Commands;

public class UpdateCurrentViewModelCommand : ICommand
{
    private readonly INavigator _navigator;
    private readonly IViewModelAbstractFactory _abstractFactory;

    public UpdateCurrentViewModelCommand(
        INavigator navigator,
        IViewModelAbstractFactory abstractFactory)
    {
        _navigator = navigator;
        _abstractFactory = abstractFactory;
    }
    
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (parameter is ViewType)
        {
            var type = (ViewType)parameter;

            _navigator.CurrentViewModel = _abstractFactory.CreateViewModel(type);
        }
    }

    public event EventHandler? CanExecuteChanged;
}