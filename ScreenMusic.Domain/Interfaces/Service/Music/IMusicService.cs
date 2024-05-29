using ScreenMusic.Arguments;

namespace ScreenMusic.Domain.Interfaces.Service;

public interface IMusicService : IBaseService<InputCreateMusic, InputUpdateMusic, OutputMusic, InputIdentifierMusic> { }