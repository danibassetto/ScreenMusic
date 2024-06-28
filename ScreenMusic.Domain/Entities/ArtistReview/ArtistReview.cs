using System.ComponentModel.DataAnnotations.Schema;

namespace ScreenMusic.Domain.Entities;

public class ArtistReview : BaseEntity<ArtistReview>
{
    public long? ArtistId { get; set; }
    public long? UserId { get; set; }
    public int? Rating { get; set; }

    public override DateTime? ChangeDate { get => base.ChangeDate; set => base.ChangeDate = value; }
    public override DateTime? CreationDate { get => base.CreationDate; set => base.CreationDate = value; }

    public virtual Artist? Artist { get; set; }
    public virtual User? User { get; set; }

    public ArtistReview() { }

    public ArtistReview(long artistId, long userId, int rating)
    {
        ArtistId = artistId;
        UserId = userId;
        Rating = rating;
    }
}