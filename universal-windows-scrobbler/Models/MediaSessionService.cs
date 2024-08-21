using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.Control;

namespace universal_windows_scrobbler.Models;

public class MediaSessionService
{
    private GlobalSystemMediaTransportControlsSessionManager _manager;
    
    public TypedEventHandler<MediaSessionService, IReadOnlyList<GlobalSystemMediaTransportControlsSession>> SessionsChanged;
    
    public MediaSessionService()
    {
        Task.Run(GetManager);
    }

    private async void GetManager()
    {
        _manager = await GlobalSystemMediaTransportControlsSessionManager.RequestAsync();
        Console.WriteLine("got the manager");
        
        // TODO this is a race condition with registering the event handler
        _manager.SessionsChanged += ManagerOnSessionsChanged;
        SessionsChanged.Invoke(this, _manager.GetSessions());
    }

    private void ManagerOnSessionsChanged(GlobalSystemMediaTransportControlsSessionManager sender, SessionsChangedEventArgs args)
    {
        Console.WriteLine("sessions changed");
        SessionsChanged.Invoke(this, _manager.GetSessions());
    }
}