using System.Windows.Input;
using System.Windows.Media.Imaging;
using UI.ViewModels;

namespace UI.Commands;

public class UploadPhotoCommand : ICommand
{
    private readonly UserProfileViewModel _userProfileViewModel;

    public UploadPhotoCommand(UserProfileViewModel userProfileViewModel)
    {
        _userProfileViewModel = userProfileViewModel;
    }
    
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        using var ofd = new System.Windows.Forms.OpenFileDialog();
        
        ofd.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";

        if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
        
        var filePath = ofd.FileName;
            
        var bitmap = new BitmapImage();
            
        bitmap.BeginInit();
            
        bitmap.UriSource = new Uri(filePath, UriKind.Absolute);
            
        bitmap.EndInit();
            
        _userProfileViewModel.Source = bitmap;
        _userProfileViewModel.AvatarFilePath = filePath;
    }

    public event EventHandler? CanExecuteChanged;
}