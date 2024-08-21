using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Media.Control;
using CommunityToolkit.Mvvm.ComponentModel;
using universal_windows_scrobbler.Models;

namespace universal_windows_scrobbler.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static

    [ObservableProperty] private string _title = "No Song Playing";
    
    private MediaSessionService _mediaSessionService; 
    
    public ObservableCollection<SessionViewModel> Sessions { get; } = [];
    
    public MainWindowViewModel()
    {
        _mediaSessionService = new MediaSessionService();
        _mediaSessionService.SessionsChanged += SessionsChanged;
    }

    private void SessionsChanged(MediaSessionService sender, IReadOnlyList<GlobalSystemMediaTransportControlsSession> args)
    {
        // TODO is this the best way to do this?

        foreach (var session in Sessions)
        {
            session.UnregisterEventHandlers();
        }
        Sessions.Clear();
        
        foreach (var session in args)
        {
            var newSessionViewModel = new SessionViewModel(session);
            newSessionViewModel.RegisterEventHandlers();
            Sessions.Add(newSessionViewModel);
        }
    }
}