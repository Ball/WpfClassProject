using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace WpfClassProject.DataContexts.RuntimeContexts
{
    public class ShellContext : IShellDataContext, INotifyPropertyChanged
    {
        public ObservableCollection<IPlaylist> Playlists { get; private set; }
        public IMediaPlayer CurrentSong { get; private set; }
        public ShellContext()
        {
            Playlists = new ObservableCollection<IPlaylist>();
            LoadPlaylists();
            CurrentSong = new WpfTunesPlayer();
            CurrentSong.PropertyChanged += StatusChanged;
        }

        private void StatusChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Status")
            {
                if(PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs( "CurrentSong"));
                }
            }
        }

        private void LoadPlaylists()
        {
            var songDirs = new DirectoryInfo(".").GetDirectories();
            foreach(var dir in songDirs)
            {
                var playlist = new Playlist {Name = dir.Name};
                var i = 1;
                foreach(var file in dir.GetFiles("*.mp3"))
                {
                    playlist.Songs.Add(new Song
                                           {
                                               Album = "Something " + dir.Name,
                                               Artist = "Someone " + dir.Name,
                                               Path = file.FullName,
                                               Title = file.Name,
                                               TrackNumber = String.Format("{0:2}", i++)
                                           });
                }
                Playlists.Add(playlist);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
