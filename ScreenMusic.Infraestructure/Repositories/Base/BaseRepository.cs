﻿using Microsoft.EntityFrameworkCore;
using ScreenMusic.Domain.Entities;
using ScreenMusic.Domain.Interfaces.Repository;
using System.Linq.Expressions;
using System.Reflection;

namespace ScreenMusic.Infraestructure.Repository;

public class BaseRepository<TEntity, TInputIdentifier>(ScreenMusicContext context) : IBaseRepository<TEntity, TInputIdentifier>
    where TEntity : BaseEntity<TEntity>, new()
{
    protected readonly ScreenMusicContext _context = context;

    #region Read
    public List<TEntity>? GetAll()
    {
        IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();
        query = BaseRepository<TEntity, TInputIdentifier>.IncludeVirtualProperties(query);
        return [.. query];
    }

    public TEntity? Get(long id)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking().Where(x => x.Id == id);
        query = BaseRepository<TEntity, TInputIdentifier>.IncludeVirtualProperties(query);
        return query.FirstOrDefault();
    }

    public TEntity? GetByIdentifier(TInputIdentifier inputIdentifier)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

        foreach (var property in typeof(TInputIdentifier).GetProperties())
        {
            var propertyName = property.Name;
            var propertyValue = property.GetValue(inputIdentifier);

            if (propertyValue != null)
            {
                var parameter = Expression.Parameter(typeof(TEntity), "x");
                var member = Expression.Property(parameter, propertyName);
                var constant = Expression.Constant(propertyValue, member.Type);

                var body = Expression.Equal(member, constant);
                var lambda = Expression.Lambda<Func<TEntity, bool>>(body, parameter);

                query = query.Where(lambda);
            }
        }

        query = BaseRepository<TEntity, TInputIdentifier>.IncludeVirtualProperties(query);

        return query.FirstOrDefault();
    }

    #endregion

    #region Create
    public long? Create(TEntity entity)
    {
        _context.Add(entity.SetCreateData());
        _context.SaveChanges();
        return entity.Id;
    }
    #endregion

    #region Update
    public long? Update(TEntity entity)
    {
        _context.Update(entity.SetUpdateData());
        _context.SaveChanges();
        return entity.Id;
    }
    #endregion

    #region Delete
    public bool Delete(TEntity entity)
    {
        _context.Remove(entity);
        _context.SaveChanges();
        return true;
    }
    #endregion

    #region InternalMethods
    protected static IQueryable<TEntity> IncludeVirtualProperties(IQueryable<TEntity> query)
    {
        var entityType = typeof(TEntity);
        var baseEntityType = typeof(BaseEntity<>).MakeGenericType(entityType);
        var properties = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                   .Where(p => p.DeclaringType == entityType &&
                                               p.GetGetMethod()?.IsVirtual == true &&
                                               !baseEntityType.GetProperties().Any(bp => bp.Name == p.Name));

        foreach (var property in properties)
        {
            query = query.Include(property.Name);
        }

        return query;
    }
    #endregion
}