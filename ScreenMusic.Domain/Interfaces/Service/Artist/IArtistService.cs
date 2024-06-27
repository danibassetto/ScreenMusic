using ScreenMusic.Arguments;

namespace ScreenMusic.Domain.Interfaces.Service;

public interface IArtistService : IBaseService<InputCreateArtist, InputUpdateArtist, OutputArtist, InputIdentifierArtist>
{
    bool Review(long id, InputReviewArtist inputReviewArtist);
    OutputArtistReview? GetReview(long id);
}