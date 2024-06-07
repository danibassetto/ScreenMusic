using System.ComponentModel.DataAnnotations;

namespace ScreenMusic.Arguments;

public class InputUpdateArtist(string profilePhoto, string biography)
{
    public string ProfilePhoto { get; private set; } = profilePhoto;
    [Required][MaxLength(500, ErrorMessage = "Quantidade de caracteres inválida")] public string Biography { get; private set; } = biography;
}