using System.ComponentModel.DataAnnotations;

namespace ScreenMusic.Arguments;

public class InputCreateMusic
{
    [Required][MaxLength(200, ErrorMessage = "Quantidade de caracteres inválida")] public string? Name { get; private set; }
    [Required] public int? ReleaseYear { get; private set; }
    [Required] public long? ArtistId { get; private set; }
    [Required] public long? MusicGenreId { get; private set; }

    public InputCreateMusic() { }

    public InputCreateMusic(string name, int releaseYear, long artistId, long musicGenreId)
    {
        Name = name;
        ReleaseYear = releaseYear;
        ArtistId = artistId;
        MusicGenreId = musicGenreId;
    }
}