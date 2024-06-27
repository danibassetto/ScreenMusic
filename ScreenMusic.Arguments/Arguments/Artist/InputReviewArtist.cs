namespace ScreenMusic.Arguments;

public class InputReviewArtist
{
    public int Rating { get; set; }

    public InputReviewArtist() { }

    public InputReviewArtist(int rating)
    {
        Rating = rating;
    }
}