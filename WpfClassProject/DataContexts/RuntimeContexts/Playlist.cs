using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WpfClassProject.DataContexts.RuntimeContexts
{
    public class Playlist :IPlaylist
    {
        public string Name { get; set; }
        public ObservableCollection<ISong> Songs { get; private set; }
        public Playlist()
        {
            Songs = new ObservableCollection<ISong>();
        }
    }
}
