using ScreenMusic.Arguments;
using ScreenMusic.Domain.ApiManagement;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Domain.Interfaces.Repository;
using ScreenMusic.Domain.Interfaces.Service;

namespace ScreenMusic.Domain.Services;

public class BaseService<TBaseRepository, TInputCreate, TInputUpdate, TEntity, TOutput, TInputIdentifier>(TBaseRepository? repository) : IBaseService<TInputCreate, TInputUpdate, TOutput, TInputIdentifier>
    where TBaseRepository : IBaseRepository<TEntity, TInputIdentifier>
    where TEntity : BaseEntity<TEntity>, new()
{
    public Guid _guidApiDataRequest;
    protected TBaseRepository? _repository = repository;
    public void SetGuid(Guid guidApiDataRequest)
    {
        _guidApiDataRequest = guidApiDataRequest;
        GenericModule.SetGuidApiDataRequest(this, guidApiDataRequest);
    }

    #region Read
    public virtual List<TOutput>? GetAll()
    {
        var entity = _repository!.GetAll();

        if (entity is not null)
            return FromEntityToOutput(entity);
        else
            return default;
    }

    public virtual TOutput? Get(long id)
    {
        var entity = _repository!.Get(id);

        if (entity is not null)
            return FromEntityToOutput(entity);
        else
            return default;
    }

    public virtual TOutput? GetByIdentifier(TInputIdentifier inputIdentifier)
    {
        var entity = _repository!.GetByIdentifier(inputIdentifier);

        if (entity is not null)
            return FromEntityToOutput(entity);
        else
            return default;
    }
    #endregion

    #region Create
    public virtual long? Create(TInputCreate inputCreate)
    {
        return _repository!.Create(FromInputCreateToEntity(inputCreate) ?? new TEntity());
    }
    #endregion

    #region Update
    public virtual long? Update(long id, TInputUpdate inputUpdate)
    {
        var oldEntity = Get(id) ?? throw new KeyNotFoundException("Id inválido ou inexistente. Processo: Update");

        var entity = BaseService<TBaseRepository, TInputCreate, TInputUpdate, TEntity, TOutput, TInputIdentifier>.UpdateEntity(FromOutputToEntity(oldEntity), inputUpdate);
        return _repository!.Update(entity ?? new TEntity());
    }

    private static TEntity? UpdateEntity(TEntity oldEntity, TInputUpdate inputUpdate)
    {
        foreach (var property in typeof(TInputUpdate).GetProperties())
        {
            var correspondingProperty = typeof(TEntity).GetProperty(property.Name);
            if (correspondingProperty != null)
            {
                var value = property.GetValue(inputUpdate, null);

                correspondingProperty?.SetValue(oldEntity, value, null);
            }
        }
        return oldEntity;
    }
    #endregion

    #region Delete
    public virtual bool Delete(long id)
    {
        var entity = Get(id) ?? throw new KeyNotFoundException("Id inválido ou inexistente. Processo: Delete");
        _repository!.Delete(FromOutputToEntity(entity));
        return true;
    }
    #endregion

    #region Mapper
    public TOutput? FromEntityToOutput(TEntity entity)
    {
        return ApiData.Mapper.MapperEntityOutput.Map<TEntity, TOutput>(entity);
    }

    public List<TOutput>? FromEntityToOutput(List<TEntity> listEntity)
    {
        return ApiData.Mapper.MapperEntityOutput.Map<List<TEntity>, List<TOutput>>(listEntity);
    }

    public TEntity FromOutputToEntity(TOutput output)
    {
        return ApiData.Mapper.MapperEntityOutput.Map<TOutput, TEntity>(output);
    }

    public TEntity FromInputCreateToEntity(TInputCreate inputCreate)
    {
        return ApiData.Mapper.MapperInputEntity.Map<TInputCreate, TEntity>(inputCreate);
    }

    public TEntity FromInputUpdateToEntity(TInputUpdate inputUpdate)
    {
        return ApiData.Mapper.MapperInputEntity.Map<TInputUpdate, TEntity>(inputUpdate);
    }
    #endregion
}