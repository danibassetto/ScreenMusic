using System.ComponentModel.DataAnnotations;

namespace ScreenMusic.Arguments;

public class InputCreateArtist(string name, string profilePhoto, string biography)
{
    [Required][MaxLength(100)] public string Name { get; private set; } = name;
    public string ProfilePhoto { get; private set; } = profilePhoto;
    [Required][MaxLength(500)] public string Biography { get; private set; } = biography;
}