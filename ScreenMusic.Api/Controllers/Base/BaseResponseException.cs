using ScreenMusic.Arguments;

namespace ScreenMusic.Api.Controllers;

public class BaseResponseException(List<Notification> _incidents) : Exception
{
    public List<Notification> Incidents { get; private set; } = _incidents;
}