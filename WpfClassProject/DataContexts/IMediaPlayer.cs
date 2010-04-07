using System;
using System.ComponentModel;

namespace WpfClassProject.DataContexts
{
    interface IMediaPlayer : INotifyPropertyChanged
    {
        string SongTitle { get; }
        double SongPosition { get; set; }
        PlayStatus Status { get; }
        void PlaySong(ISong song);
        void Pause();
        void Stop();
        TimeSpan Length { get; }
        TimeSpan Position { get; }
    }
}