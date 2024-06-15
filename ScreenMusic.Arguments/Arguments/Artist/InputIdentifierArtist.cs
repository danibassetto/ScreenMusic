using System.ComponentModel.DataAnnotations;

namespace ScreenMusic.Arguments;

public class InputIdentifierArtist
{
    [Required][MaxLength(100, ErrorMessage = "Quantidade de caracteres inválida")] public string? Name { get; private set; }

    public InputIdentifierArtist()    {    }
    
    public InputIdentifierArtist(string name)
    {
        Name = name;
    }
}