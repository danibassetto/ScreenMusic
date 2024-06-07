namespace ScreenMusic.Arguments;

public class InputCreateMusic(string name, int releaseYear, long artistId, long musicGenreId)
{
    public string Name { get; private set; } = name;
    public int ReleaseYear { get; private set; } = releaseYear;
    public long ArtistId { get; private set; } = artistId;
    public long MusicGenreId { get; private set; } = musicGenreId;
}