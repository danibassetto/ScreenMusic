using ScreenMusic.Arguments;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Domain.Interfaces.Repository;
using ScreenMusic.Domain.Interfaces.Service;

namespace ScreenMusic.Domain.Services;

public class UserService(IUserRepository repository) : BaseService_1<IUserRepository, InputCreateUser, InputUpdateUser, User, OutputUser, InputIdentifierUser>(repository), IUserService { }