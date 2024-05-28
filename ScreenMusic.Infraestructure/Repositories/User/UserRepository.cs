using ScreenMusic.Arguments;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Domain.Interfaces.Repository;

namespace ScreenMusic.Infraestructure.Repository;

public class UserRepository(ScreenMusicContext context) : BaseRepository<User, InputIdentifierUser>(context), IUserRepository { }