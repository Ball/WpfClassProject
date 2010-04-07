using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace WpfClassProject.DataContexts.DesignTimeContexts
{
    class ShellDesignData : IShellDataContext
    {
        public ObservableCollection<IPlaylist> Playlists { get; private set; }
        public IMediaPlayer CurrentSong { get; private set; }
        public  ShellDesignData()
        {
            var playlistOne = new PlaylistDesignData { Name = "Playlist One"};
            playlistOne.Songs.Add(new SongDesignData{ Artist = "Some Artist", Album = "Debut", Title = "Septimum", TrackNumber = "07"});
            playlistOne.Songs.Add(new SongDesignData{ Artist = "Some Artist", Album = "Sophomore Jinx", Title = "The year begins again", TrackNumber = "01"});
            var playlistTwo = new PlaylistDesignData {Name = "lorem"};
            for( var i=0; i < 10; i++)
            {
                playlistTwo.Songs.Add(new SongDesignData { Artist = "Lorem", Album = "Ipsum", Title = "dolor sit amet", TrackNumber = String.Format("{0:2}", i)});
            }

            var totalPlaylist = new PlaylistDesignData {Name = "All Songs"};

            
            foreach(var song in playlistOne.Songs.Concat(playlistTwo.Songs))
            {
                totalPlaylist.Songs.Add(song);
            }

            Playlists = new ObservableCollection<IPlaylist>(
                            new List<IPlaylist>{
                                totalPlaylist,
                                playlistOne,
                                playlistTwo
                            }
                );
            CurrentSong = new CurrentSongDesignData();
        }
    }
}
