namespace ScreenMusic.Arguments;

public class OutputArtistReview
{
    public long? ArtistId { get; set; }
    public long? UserId { get; set; }
    public int? Rating { get; set; }
    public OutputArtist? Artist { get; set; }
}