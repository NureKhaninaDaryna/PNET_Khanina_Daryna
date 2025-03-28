using UI.Enums;

namespace UI.ViewModels.Factories;

public interface IViewModelAbstractFactory
{
    ViewModelBase CreateViewModel(ViewType viewType);
}