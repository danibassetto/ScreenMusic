using System.ComponentModel.DataAnnotations;

namespace ScreenMusic.Arguments;

public class InputUpdateMusicGenre(string description)
{
    [Required][MaxLength(300, ErrorMessage = "Quantidade de caracteres inválida")] public string Description { get; private set; } = description;
}