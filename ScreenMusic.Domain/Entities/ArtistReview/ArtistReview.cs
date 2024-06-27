using System.ComponentModel.DataAnnotations.Schema;

namespace ScreenMusic.Domain.Entities;

public class ArtistReview
{
    public long? ArtistId { get; set; }
    public long? UserId { get; set; }
    public int? Rating { get; set; }

    public virtual Artist? Artist { get; set; }

    public ArtistReview() { }

    public ArtistReview(long artistId, long userId, int rating)
    {
        ArtistId = artistId;
        UserId = userId;
        Rating = rating;
    }
}