namespace ScreenMusic.Arguments;

public class OutputArtist
{
    public long Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ChangeDate { get; set; }
    public string? Name { get; set; }
    public string? ProfilePhoto { get; set; }
    public string? Biography { get; set; }
    public decimal? Classification { get; set; }
    public ICollection<OutputMusic>? ListMusic { get; set; }
    public ICollection<OutputArtistReview>? ListArtistReview { get; set; }
}