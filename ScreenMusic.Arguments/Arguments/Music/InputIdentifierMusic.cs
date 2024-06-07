using System.ComponentModel.DataAnnotations;

namespace ScreenMusic.Arguments;

public class InputIdentifierMusic(string name)
{
    [Required][MaxLength(200, ErrorMessage = "Quantidade de caracteres inválida")] public string Name { get; private set; } = name;
}