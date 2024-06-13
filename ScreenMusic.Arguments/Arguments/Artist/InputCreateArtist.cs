using System.ComponentModel.DataAnnotations;

namespace ScreenMusic.Arguments;

public class InputCreateArtist
{
    [Required][MaxLength(100)] public string? Name { get; set; }
    public string? ProfilePhoto { get; set; }
    [Required][MaxLength(500)] public string? Biography { get; set; }

    public InputCreateArtist() { }

    public InputCreateArtist(string name, string profilePhoto, string biography)
    {
        Name = name;
        ProfilePhoto = profilePhoto;
        Biography = biography;
    }
}