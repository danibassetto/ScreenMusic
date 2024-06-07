namespace ScreenMusic.Arguments;

public class InputUpdateMusicGenre(string description)
{
    public string Description { get; private set; } = description;
}