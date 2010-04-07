namespace WpfClassProject.DataContexts.DesignTimeContexts
{
    internal class SongDesignData : ISong
    {
        public string Artist { get; set; }
        public string Album { get; set; }
        public string TrackNumber { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
    }
}