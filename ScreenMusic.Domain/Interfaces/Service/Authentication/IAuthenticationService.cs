using ScreenMusic.Arguments;

namespace ScreenMusic.Domain.Interfaces.Service;

public interface IAuthenticationService : IBaseService_0
{
    OutputAuthentication? Authenticate(InputAuthentication inputAuthentication);
}