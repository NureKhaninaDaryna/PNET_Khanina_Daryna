using System.Windows;
using System.Windows.Input;
using Domain.Models;

namespace UI.Views;

public partial class AddPackageWindow : Window
{
    public AddPackageWindow()
    {
        InitializeComponent();
        DataContext = this;

        AddPackageCommand = new RelayCommand(AddPackage);
    }
    
    public Package NewPackage { get; set; } = new();

    public ICommand AddPackageCommand { get; }

    private void AddPackage()
    {
        DialogResult = true;
        Close();
    }
}
