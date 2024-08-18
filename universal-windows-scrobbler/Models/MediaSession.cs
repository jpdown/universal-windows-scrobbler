using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Sensors;
using Windows.Media.Control;
using ABI.Windows.ApplicationModel.UserDataTasks;
using ABI.Windows.Security.Authentication.Identity.Core;

namespace universal_windows_scrobbler.Models;

public class MediaSession
{
    private GlobalSystemMediaTransportControlsSessionManager _manager;
    private GlobalSystemMediaTransportControlsSession _session;
    
    // TODO song model instead
    public delegate void SongTitleHandler(MediaSession sender, string title);
    public event SongTitleHandler SongTitleChanged;

    public MediaSession()
    {
        Task.Run(GetManager);
    }

    private async void GetManager()
    {
        _manager = await GlobalSystemMediaTransportControlsSessionManager.RequestAsync();
        Console.WriteLine("got the manager");

        _manager.CurrentSessionChanged += HandleSessionChanged;
        Console.WriteLine("Listening for new session");
        RegisterSession(_manager.GetCurrentSession());
    }

    private void HandleSessionChanged(GlobalSystemMediaTransportControlsSessionManager sender, CurrentSessionChangedEventArgs args)
    {
        Console.WriteLine("Got a new session");
        if (_session != null)
        {
            DeregisterSession(_session);
        }
        
        _session = sender.GetCurrentSession();
        RegisterSession(_session);
    }

    private void RegisterSession(GlobalSystemMediaTransportControlsSession session)
    {
        session.MediaPropertiesChanged += SessionOnMediaPropertiesChanged;
        session.PlaybackInfoChanged += SessionOnPlaybackInfoChanged;
        session.TimelinePropertiesChanged += SessionOnTimelinePropertiesChanged;
        Console.WriteLine($"got session {session}");
    }

    private void DeregisterSession(GlobalSystemMediaTransportControlsSession session)
    {
        session.MediaPropertiesChanged -= SessionOnMediaPropertiesChanged;
        session.PlaybackInfoChanged -= SessionOnPlaybackInfoChanged;
        session.TimelinePropertiesChanged -= SessionOnTimelinePropertiesChanged;
    }

    private void SessionOnTimelinePropertiesChanged(GlobalSystemMediaTransportControlsSession sender, TimelinePropertiesChangedEventArgs args)
    {
    }

    private void SessionOnPlaybackInfoChanged(GlobalSystemMediaTransportControlsSession sender, PlaybackInfoChangedEventArgs args)
    {
    }

    private async void SessionOnMediaPropertiesChanged(GlobalSystemMediaTransportControlsSession sender, MediaPropertiesChangedEventArgs args)
    {
        var mediaProperties = await sender.TryGetMediaPropertiesAsync();
        if (mediaProperties == null) return;
        
        Console.WriteLine("media properties changed");
        
        SongTitleChanged.Invoke(this, mediaProperties.Title);
    }
}