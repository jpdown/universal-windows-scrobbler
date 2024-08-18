using CommunityToolkit.Mvvm.ComponentModel;
using Tmds.DBus.Protocol;
using universal_windows_scrobbler.Models;

namespace universal_windows_scrobbler.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static

    [ObservableProperty] private string _title = "No Song Playing";
    
    private MediaSession _mediaSession; 
    
    public MainWindowViewModel()
    {
        _mediaSession = new MediaSession();
        _mediaSession.SongTitleChanged += MediaSessionOnSongTitleChanged;
    }

    private void MediaSessionOnSongTitleChanged(MediaSession sender, string title)
    {
        Title = title;
    }
}