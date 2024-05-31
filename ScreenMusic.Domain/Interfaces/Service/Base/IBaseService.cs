using ScreenMusic.Arguments;

namespace ScreenMusic.Domain.Interfaces.Service;

public interface IBaseService<TInputCreate, TInputUpdate, TOutput, TInputIdentifier>
{
    IReadOnlyCollection<Notification> ListNotification { get; }
    List<TOutput>? GetAll();
    TOutput? Get(long id);
    TOutput? GetByIdentifier(TInputIdentifier inputIdentifier);
    long? Create(TInputCreate entity);
    long? Update(long id, TInputUpdate inputUpdate);
    bool Delete(long id);
}

#region AllParameters
public interface IBaseService_0 : IBaseService<object, object, object, object> { }
#endregion