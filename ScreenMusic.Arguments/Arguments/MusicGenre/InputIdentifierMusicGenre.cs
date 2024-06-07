using System.ComponentModel.DataAnnotations;

namespace ScreenMusic.Arguments;

public class InputIdentifierMusicGenre(string name)
{
    [Required][MaxLength(150, ErrorMessage = "Quantidade de caracteres inválida")] public string Name { get; private set; } = name;
}