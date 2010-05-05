using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using WpfClassProject.Commands;

namespace WpfClassProject.DataContexts.RuntimeContexts
{
    public class WpfTunesPlayer : IMediaPlayer
    {

        private MediaPlayer _player;
        private ISong _currentSong;
        private DispatcherTimer _dispatcherTimer;

        private ICommand _playCommand;
        private ICommand _pauseCommand;
        private ICommand _stopCommand;
        private ICommand _nextCommand;
        private ICommand _backCommand;
        public event PropertyChangedEventHandler PropertyChanged;

        public TimeSpan Length
        {
            get { return _player.NaturalDuration.TimeSpan; }
        }

        public TimeSpan Position
        {
            get { return _player.Position; }
        }

        private IPlaylist _currentPlaylist;
        public IPlaylist CurrentPlaylist
        {
            get { return _currentPlaylist; }
            private set
            {
                _currentPlaylist = value;
                NotifyPropertyChanged("CurrentPlaylist");
            }
        }

        private string _songTitle;
        public string SongTitle
        {
            get { return _songTitle; }
            private set
            {
                _songTitle = value;
                NotifyPropertyChanged("SongTitle");
            }
        }

        public double SongPosition
        {
            get
            {
                return (100.0d * _player.Position.TotalMilliseconds) / _player.NaturalDuration.TimeSpan.TotalMilliseconds;
            }
            set
            {
                _player.Position =
                    TimeSpan.FromMilliseconds(value * _player.NaturalDuration.TimeSpan.TotalMilliseconds / 100.0d);
                UpdatePosition();
            }
        }

        private PlayStatus _status;
        public PlayStatus Status
        {
            get { return _status; }
            private set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public WpfTunesPlayer()
        {
            _playCommand = new PlayCommand();
            _pauseCommand = new PauseCommand();
            _stopCommand = new StopCommand();
            _nextCommand = new NextCommand();
            _backCommand = new BackCommand();
            _dispatcherTimer = new DispatcherTimer{Interval = TimeSpan.FromMilliseconds(100)};
            _dispatcherTimer.Tick += (sender, eventArg) => UpdatePosition();
            _player = new MediaPlayer();
            _player.MediaEnded += (sender, arg) => Next();
            Status = PlayStatus.Stopped;
        }

        private void UpdatePosition()
        {
            NotifyPropertyChanged("Length");
            NotifyPropertyChanged("Position");
            NotifyPropertyChanged("SongPosition");
        }


        public ICommand PlayCommand
        {
            get { return _playCommand; }
        }

        public void PlaySong(ISong song, IPlaylist currentPlaylist)
        {
            if(currentPlaylist == null)
            {
                return;
            }
            if(song == null)
            {
                song = currentPlaylist.Songs.First();
            }

            if(Status == PlayStatus.Paused)
            {
                _player.Play();
                _dispatcherTimer.IsEnabled = true;
                Status = PlayStatus.Playing;

            }
            else if(Status == PlayStatus.Stopped)
            {
                PlayASong(song, currentPlaylist);
                CurrentPlaylist = currentPlaylist;
            }
            else if (CurrentPlaylist != currentPlaylist)
            {
                PlayASong(song, currentPlaylist);
                CurrentPlaylist = currentPlaylist;
            }
        }

        private void PlayASong(ISong song, IPlaylist currentPlaylist)
        {
            _currentSong = song;
            _player.Open(new Uri(song.Path));
            SongTitle = song.Title;
            _player.Play();
            _dispatcherTimer.IsEnabled = true;
            Status = PlayStatus.Playing;
        }

        public ICommand PauseCommand
        {
            get { return _pauseCommand; }
        }

        public void Pause()
        {
            _player.Pause();
            _dispatcherTimer.IsEnabled = false;
            Status = PlayStatus.Paused;
            UpdatePosition();
        }

        public ICommand StopCommand
        {
            get { return _stopCommand; }
        }

        public void Stop()
        {
            _player.Stop();
            _dispatcherTimer.IsEnabled = false;
            Status = PlayStatus.Stopped;
            SongTitle = "";
            UpdatePosition();
        }

        public ICommand NextCommand
        {
            get { return _nextCommand; }
        }

        public void Next()
        {
            ISong song;
            if (_currentSong == null && CurrentPlaylist != null)
            {
                song = CurrentPlaylist.Songs.First();
            }
            else if(CurrentPlaylist != null)
            {
                song = CurrentPlaylist.Songs.SkipWhile(s => s != _currentSong).Skip(1).FirstOrDefault();
                if(song == null)
                {
                    Stop();
                    return;
                }
            }
            else
            {
                return;
            }
            PlayASong(song, CurrentPlaylist);
        }

        public ICommand BackCommand
        {
            get { return _backCommand; }
        }

        public void Back()
        {
            ISong song;
            if(_currentSong == null && CurrentPlaylist != null)
            {
                song = CurrentPlaylist.Songs.First();
            }
            else if (CurrentPlaylist != null)
            {
                song = CurrentPlaylist.Songs.TakeWhile(s => s != _currentSong).LastOrDefault();
                if(song == null)
                    return;
            }
            else
            {
                return;
            }
            PlayASong(song, CurrentPlaylist);
        }
    }
}
