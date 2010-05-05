using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfClassProject.DataContexts.RuntimeContexts
{
    public class Song : ISong
    {
        public string Artist { get; set; }
        public string Album { get; set; }
        public string TrackNumber { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
    }
}
