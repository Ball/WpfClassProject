using System;
using System.ComponentModel;

namespace WpfClassProject.DataContexts.DesignTimeContexts
{
    internal class CurrentSongDesignData : IMediaPlayer
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CurrentSongDesignData()
        {
            SongTitle = "Pocket full of change";
            SongPosition = 15;
            Status = PlayStatus.Paused;
            Length = TimeSpan.FromSeconds(150);
            Position = TimeSpan.FromSeconds(78);
        }

        private double _songPosition;
        public double SongPosition
        {
            get { return _songPosition; }
            set
            {
                if(_songPosition != value)
                {
                    _songPosition = value;   
                    NotifyChangeTo("SongPosition");
                }
            }
        }
        public IPlaylist CurrentPlaylist { get; private set; }
        public string SongTitle { get; private set; }
        public PlayStatus Status { get; private set; }
        public TimeSpan Length { get; private set; }
        public TimeSpan Position { get; private set; }

        private void NotifyChangeTo(string propertyName)
        {
            if( PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void PlaySong(ISong song, IPlaylist currentPlaylist)
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Next()
        {
            throw new NotImplementedException();
        }

        public void Back()
        {
            throw new NotImplementedException();
        }
    }
}