namespace ScreenMusic.Arguments;

public class OutputMusicGenre
{
    public long Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ChangeDate { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ICollection<OutputMusic>? ListMusic { get; set; }
}