using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UI.Views;

public partial class RegisterView : UserControl
{
    private static readonly DependencyProperty RegisterCommandProperty =
        DependencyProperty.Register(nameof(RegisterCommand), typeof(ICommand), typeof(RegisterView), new PropertyMetadata(null));

    public ICommand? RegisterCommand
    {
        get => (ICommand)GetValue(RegisterCommandProperty);
        set => SetValue(RegisterCommandProperty, value);
    }
    
    public RegisterView()
    {
        InitializeComponent();
    }

    private void RegisterBtn_OnClick(object sender, RoutedEventArgs e)
    {
        if (RegisterCommand != null)
        {
            RegisterCommand.Execute(PbPassword.Password);
        }
    }
}