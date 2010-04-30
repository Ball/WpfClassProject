using System;
using System.ComponentModel;

namespace WpfClassProject.DataContexts
{
    public interface IMediaPlayer : INotifyPropertyChanged
    {
        IPlaylist CurrentPlaylist { get; }
        string SongTitle { get; }
        double SongPosition { get; set; }
        PlayStatus Status { get; }
        void PlaySong(ISong song, IPlaylist currentPlaylist);
        void Pause();
        void Stop();
        void Next();
        void Back();
        TimeSpan Length { get; }
        TimeSpan Position { get; }
    }
}