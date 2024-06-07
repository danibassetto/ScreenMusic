using System.ComponentModel.DataAnnotations;

namespace ScreenMusic.Arguments;

public class InputIdentifierArtist(string name)
{
    [Required][MaxLength(100, ErrorMessage = "Quantidade de caracteres inválida")] public string Name { get; private set; } = name;
}