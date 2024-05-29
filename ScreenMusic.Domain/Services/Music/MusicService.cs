using ScreenMusic.Arguments;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Domain.Interfaces.Repository;
using ScreenMusic.Domain.Interfaces.Service;

namespace ScreenMusic.Domain.Services;

public class MusicService(IMusicRepository repository) : BaseService_1<IMusicRepository, InputCreateMusic, InputUpdateMusic, Music, OutputMusic, InputIdentifierMusic>(repository), IMusicService { }