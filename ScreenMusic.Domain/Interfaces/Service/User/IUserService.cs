using ScreenMusic.Arguments;

namespace ScreenMusic.Domain.Interfaces.Service;

public interface IUserService : IBaseService<InputCreateUser, InputUpdateUser, OutputUser, InputIdentifierUser> { }