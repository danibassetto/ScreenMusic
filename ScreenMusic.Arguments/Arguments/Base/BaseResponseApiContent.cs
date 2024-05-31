using Newtonsoft.Json;

namespace ScreenMusic.Arguments;

public class BaseResponseApiContent<TTypeResult>
{
    public TTypeResult? Result { get; set; }
    public List<Notification>? ListNotification { get; set; }
    [JsonIgnore] public int StatusCode { get; set; }
}