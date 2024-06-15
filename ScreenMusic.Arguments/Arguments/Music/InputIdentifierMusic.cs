using System.ComponentModel.DataAnnotations;

namespace ScreenMusic.Arguments;

public class InputIdentifierMusic
{
    [Required][MaxLength(200, ErrorMessage = "Quantidade de caracteres inválida")] public string? Name { get; set; }

    public InputIdentifierMusic() { }

    public InputIdentifierMusic(string name)
    {
        Name = name;
    }
}