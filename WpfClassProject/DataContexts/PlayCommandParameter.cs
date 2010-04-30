using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfClassProject.DataContexts
{
    public class PlayCommandParameter
    {
        public IPlaylist SelectedPlaylist { get; set; }
        public ISong SelectedSong { get; set; }
        public IMediaPlayer CurrentSong { get; set; }
    }
}
