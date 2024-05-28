using ScreenMusic.Domain.ApiManagement;
using System.Text.Json.Serialization;

namespace ScreenMusic.Domain.Entities;

public class BaseEntity<TEntity> : BaseSetProperty<TEntity>
    where TEntity : BaseEntity<TEntity>
{
    [JsonIgnore] public long? Id { get; set; }
    [JsonIgnore] public virtual DateTime? CreationDate { get; set; }
    [JsonIgnore] public virtual DateTime? ChangeDate { get; set; }

    public TEntity SetCreateData()
    {
        CreationDate = DateTime.Now;

        return (this as TEntity)!;
    }

    public TEntity SetUpdateData()
    {
        ChangeDate = DateTime.Now;

        return (this as TEntity)!;
    }
}

public class BaseEntity_0 : BaseEntity<BaseEntity_0> { }