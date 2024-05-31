namespace ScreenMusic.Arguments;

public class BaseResponseApi<TTypeResult>
{
    public BaseResponseApiContent<TTypeResult>? Value { get; set; }
}