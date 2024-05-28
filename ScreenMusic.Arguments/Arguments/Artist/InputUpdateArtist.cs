namespace ScreenMusic.Arguments;

public class InputUpdateArtist(string name, string profilePhoto, string biography)
{
    public string Name { get; private set; } = name;
    public string ProfilePhoto { get; private set; } = profilePhoto;
    public string Biography { get; private set; } = biography;
}