using System.ComponentModel.DataAnnotations;

namespace ScreenMusic.Arguments;

public class InputIdentifierMusicGenre
{
    [Required][MaxLength(150, ErrorMessage = "Quantidade de caracteres inválida")] public string? Name { get; set; }

    public InputIdentifierMusicGenre() { }

    public InputIdentifierMusicGenre(string name)
    {
        Name = name;
    }
}