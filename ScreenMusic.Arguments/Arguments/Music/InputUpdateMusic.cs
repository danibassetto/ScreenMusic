using System.ComponentModel.DataAnnotations;

namespace ScreenMusic.Arguments;

public class InputUpdateMusic
{
    [Required][MaxLength(200, ErrorMessage = "Quantidade de caracteres inválida")] public string? Name { get; set; }
    [Required] public int? ReleaseYear { get; set; }
    [Required] public long? ArtistId { get; set; }
    [Required] public long? MusicGenreId { get; set; }

    public InputUpdateMusic() { }

    public InputUpdateMusic(string? name, int? releaseYear, long? artistId, long? musicGenreId)
    {
        Name = name;
        ReleaseYear = releaseYear;
        ArtistId = artistId;
        MusicGenreId = musicGenreId;
    }
}