using ScreenMusic.Domain.Services;

namespace ScreenMusic.Api.Controllers;

public class BaseResponseApi<TTypeResult, TTypeException>
{
    public BaseResponseApiContent<TTypeResult, TTypeException>? Value { get; set; }
}