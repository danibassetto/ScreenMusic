namespace ScreenMusic.Domain.Entities;

public class Music : BaseEntity<Music>
{
    public string? Name { get; set; }
    public int? ReleaseYear { get; set; }
    public long? ArtistId { get; set; }
    public long? MusicGenreId { get; set; }
    public string? YoutubeLink { get; set; }

    public virtual Artist? Artist { get; set; }
    public virtual MusicGenre? MusicGenre { get; set; }

    public Music() { }

    public Music(string name, int releaseYear, long artistId, long musicGenreId, string? youtubeLink)
    {
        Name = name;
        ReleaseYear = releaseYear;
        ArtistId = artistId;
        MusicGenreId = musicGenreId;
        YoutubeLink = youtubeLink;
    }
}