using System.ComponentModel.DataAnnotations;

namespace ScreenMusic.Arguments;

public class InputIdentifierArtistReview
{
    [Required] public long? ArtistId { get; set; }
    [Required] public long? UserId { get; set; }

    public InputIdentifierArtistReview() { }

    public InputIdentifierArtistReview(long artistId, long userId)
    {
        ArtistId = artistId;
        UserId = userId;
    }
}