using System.Collections.ObjectModel;

namespace WpfClassProject.DataContexts
{
    interface IPlaylist
    {
        string Name { get; }
        ObservableCollection<ISong> Songs { get; }
    }
}