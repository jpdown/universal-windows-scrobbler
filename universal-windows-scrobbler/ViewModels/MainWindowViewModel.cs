using System.Collections.ObjectModel;
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
    
    public ObservableCollection<SessionViewModel> Sessions { get; } = [];
    
    public MainWindowViewModel()
    {
        _mediaSession = new MediaSession();
        _mediaSession.SongTitleChanged += MediaSessionOnSongTitleChanged;
        Sessions.Add(new SessionViewModel());
        Sessions.Add(new SessionViewModel());
        Sessions.Add(new SessionViewModel());
        Sessions.Add(new SessionViewModel());
    }

    private void MediaSessionOnSongTitleChanged(MediaSession sender, string title)
    {
        Title = title;
    }
}