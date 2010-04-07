using System.Collections.ObjectModel;

namespace WpfClassProject.DataContexts.DesignTimeContexts
{
    internal class PlaylistDesignData : IPlaylist
    {
        public string Name { get; set; }

        public ObservableCollection<ISong> Songs { get; private set; }
        public PlaylistDesignData()
        {
            Songs = new ObservableCollection<ISong>();
        }
    }
}