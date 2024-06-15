using System.ComponentModel.DataAnnotations;

namespace ScreenMusic.Arguments;

public class InputCreateMusicGenre
{
    [Required][MaxLength(100, ErrorMessage = "Quantidade de caracteres inválida")] public string? Name { get; private set; }
    [Required][MaxLength(300, ErrorMessage = "Quantidade de caracteres inválida")] public string? Description { get; private set; }

    public InputCreateMusicGenre() { }

    public InputCreateMusicGenre(string name, string description)
    {
        Name = name;
        Description = description;
    }
}