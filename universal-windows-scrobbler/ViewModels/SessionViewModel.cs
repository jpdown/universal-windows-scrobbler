using System;
using System.Collections.Generic;
using Windows.Media;
using Windows.Media.Control;
using CommunityToolkit.Mvvm.ComponentModel;

namespace universal_windows_scrobbler.ViewModels;

public partial class SessionViewModel : ViewModelBase
{
    [ObservableProperty] private string _sessionName;
    
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _artist;
    [ObservableProperty] private string _album;
    [ObservableProperty] private IReadOnlyList<string> _genres;
    [ObservableProperty] private string _subtitle;
    [ObservableProperty] private string _albumArtist;
    [ObservableProperty] private int _trackNumber;
    [ObservableProperty] private int _trackCount;
    [ObservableProperty] private MediaPlaybackType? _playbackType;
    
    [ObservableProperty] private GlobalSystemMediaTransportControlsSessionPlaybackStatus _playbackStatus;

    [ObservableProperty] private TimeSpan _position;
    [ObservableProperty] private TimeSpan _endTime;
    [ObservableProperty] private TimeSpan _startTime;
    
    private GlobalSystemMediaTransportControlsSession _session;

    public SessionViewModel(GlobalSystemMediaTransportControlsSession session)
    {
        _session = session;
        SessionName = session.SourceAppUserModelId;
    }

    public void RegisterEventHandlers()
    {
        _session.MediaPropertiesChanged += SessionOnMediaPropertiesChanged;
        _session.PlaybackInfoChanged += SessionOnPlaybackInfoChanged;
        _session.TimelinePropertiesChanged += SessionOnTimelinePropertiesChanged;
    }
    
    public void UnregisterEventHandlers()
    {
        _session.MediaPropertiesChanged -= SessionOnMediaPropertiesChanged;
        _session.PlaybackInfoChanged -= SessionOnPlaybackInfoChanged;
        _session.TimelinePropertiesChanged -= SessionOnTimelinePropertiesChanged;
    }

    private void SessionOnTimelinePropertiesChanged(GlobalSystemMediaTransportControlsSession sender, TimelinePropertiesChangedEventArgs args)
    {
        var timelineProperties = sender.GetTimelineProperties();
        Position = timelineProperties.Position;
        StartTime = timelineProperties.StartTime;
        EndTime = timelineProperties.EndTime;
        
    }

    private void SessionOnPlaybackInfoChanged(GlobalSystemMediaTransportControlsSession sender, PlaybackInfoChangedEventArgs args)
    {
        var playbackInfo = sender.GetPlaybackInfo();
        PlaybackStatus = playbackInfo.PlaybackStatus;
    }

    private async void SessionOnMediaPropertiesChanged(GlobalSystemMediaTransportControlsSession sender, MediaPropertiesChangedEventArgs args)
    {
        var mediaProperties = await sender.TryGetMediaPropertiesAsync();
        
        Title = mediaProperties.Title;
        Artist = mediaProperties.Artist;
        AlbumArtist = mediaProperties.AlbumArtist;
        Album = mediaProperties.AlbumTitle;
        Genres = mediaProperties.Genres;
        Subtitle = mediaProperties.Subtitle;
        TrackNumber = mediaProperties.TrackNumber;
        TrackCount = mediaProperties.AlbumTrackCount;
        PlaybackType = mediaProperties.PlaybackType;
    }
}