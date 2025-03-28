using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UI.Views;

public partial class LoginView : UserControl
{
    private static readonly DependencyProperty LoginCommandProperty =
        DependencyProperty.Register(nameof(LoginCommand), typeof(ICommand), typeof(LoginView), new PropertyMetadata(null));

    public ICommand? LoginCommand
    {
        get => (ICommand)GetValue(LoginCommandProperty);
        set => SetValue(LoginCommandProperty, value);
    }
    
    public LoginView()
    {
        InitializeComponent();
    }

    private void LoginBtn_OnClick(object sender, RoutedEventArgs e)
    {
        if (LoginCommand != null)
        {
            LoginCommand.Execute(PbPassword.Password);
        }
    }
}