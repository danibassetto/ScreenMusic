using System.ComponentModel.DataAnnotations;

namespace ScreenMusic.Arguments;

public class InputUpdateMusic(string name, int releaseYear, long artistId, long musicGenreId)
{
    [Required][MaxLength(200, ErrorMessage = "Quantidade de caracteres inválida")] public string Name { get; private set; } = name;
    [Required] public int ReleaseYear { get; private set; } = releaseYear;
    [Required] public long ArtistId { get; private set; } = artistId;
    [Required] public long MusicGenreId { get; private set; } = musicGenreId;
}