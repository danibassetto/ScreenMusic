using Microsoft.AspNetCore.Identity;

namespace ScreenMusic.Domain.Entities;

public class User : IdentityUser<long> 
{
    public virtual ICollection<ArtistReview>? ListArtistReview { get; set; } = [];
}