using UI.Models;

namespace UI.ViewModels;

public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;

public abstract class ViewModelBase : ObservableObject
{
    
}