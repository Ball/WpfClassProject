using System.Collections.ObjectModel;

namespace WpfClassProject.DataContexts
{
    public interface IPlaylist
    {
        string Name { get; }
        ObservableCollection<ISong> Songs { get; }
    }
}