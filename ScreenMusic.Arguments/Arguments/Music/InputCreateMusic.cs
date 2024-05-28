namespace ScreenMusic.Arguments;

public class InputCreateMusic(string name, int releaseYear, long artistaId)
{
    public string Name { get; private set; } = name;
    public int ReleaseYear { get; private set; } = releaseYear;
    public long ArtistaId { get; private set; } = artistaId;
}