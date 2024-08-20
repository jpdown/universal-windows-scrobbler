using System;
using System.Collections.Generic;
using Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace universal_windows_scrobbler.ViewModels;

public partial class SessionViewModel : ViewModelBase
{
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _artist;
    [ObservableProperty] private string _album;
    [ObservableProperty] private IReadOnlyList<string> _genres;
    [ObservableProperty] private string _subtitle;
    [ObservableProperty] private string _albumArtist;
    [ObservableProperty] private int _trackNumber;
    [ObservableProperty] private int _trackCount;
    [ObservableProperty] private MediaPlaybackType _playbackType;
    
    [ObservableProperty] private MediaPlaybackStatus _playbackStatus;

    [ObservableProperty] private TimeSpan _position;
    [ObservableProperty] private TimeSpan _endTime;
    [ObservableProperty] private TimeSpan _startTime;

    public SessionViewModel()
    {
        _title = "lmao song";
        _artist = "Lmao";
        _album = "Lmao album";
        _genres = new List<string> { "genre1", "genre2" };
        _subtitle = "subtitle";
        _albumArtist = "albumArtist";
        _trackCount = 45;
        _trackNumber = 2;
        _playbackType = MediaPlaybackType.Music;

        _playbackStatus = MediaPlaybackStatus.Playing;
        
        _position = TimeSpan.FromSeconds(45);
        _startTime = TimeSpan.FromSeconds(0);
        _endTime = TimeSpan.FromSeconds(65);
    }
}