namespace ScreenMusic.Arguments;

public class OutputMusic
{
    public long Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ChangeDate { get; set; }
    public string? Name { get; set; }
    public int ReleaseYear { get; set; }
    public long ArtistaId { get; set; }
    public long MusicGenreId { get; set; }
}