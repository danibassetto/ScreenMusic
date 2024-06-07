using System.ComponentModel.DataAnnotations;

namespace ScreenMusic.Arguments;

public class InputCreateMusicGenre(string name, string description)
{
    [Required][MaxLength(100, ErrorMessage = "Quantidade de caracteres inválida")] public string Name { get; private set; } = name;
    [Required][MaxLength(300, ErrorMessage = "Quantidade de caracteres inválida")] public string Description { get; private set; } = description;
}