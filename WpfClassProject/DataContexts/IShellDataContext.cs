using System.Collections.ObjectModel;

namespace WpfClassProject.DataContexts
{
    interface IShellDataContext
    {
        ObservableCollection<IPlaylist> Playlists { get; }
        IMediaPlayer CurrentSong { get; }
    }
}
