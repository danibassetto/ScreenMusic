using System.ComponentModel.DataAnnotations;

namespace ScreenMusic.Arguments;

public class InputUpdateMusicGenre
{
    [Required][MaxLength(300, ErrorMessage = "Quantidade de caracteres inválida")] public string? Description { get; set; }

    public InputUpdateMusicGenre() { }

    public InputUpdateMusicGenre(string description)
    {
        Description = description;
    }
}