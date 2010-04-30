namespace WpfClassProject.DataContexts
{
    public interface ISong
    {
        string Artist { get; }
        string Album { get; }
        string TrackNumber { get; }
        string Title { get; }
        string Path { get; }
    }
}