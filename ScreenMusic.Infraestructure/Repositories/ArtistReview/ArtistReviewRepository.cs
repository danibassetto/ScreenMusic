using ScreenMusic.Arguments;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Domain.Interfaces.Repository;

namespace ScreenMusic.Infraestructure.Repository;

public class ArtistReviewRepository(ScreenMusicContext context) : BaseRepository<ArtistReview, InputIdentifierArtistReview>(context), IArtistReviewRepository { }