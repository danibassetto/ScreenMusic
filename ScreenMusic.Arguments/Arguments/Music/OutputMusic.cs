namespace ScreenMusic.Arguments;

public class OutputMusic
{
    public long Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ChangeDate { get; set; }
    public string? Name { get; set; }
    public int ReleaseYear { get; set; }
    public long ArtistId { get; set; }
    public long MusicGenreId { get; set; }
    public string? YoutubeLink { get; set; }
    public OutputArtist? Artist { get; set; }
    public OutputMusicGenre? MusicGenre { get; set; }
}