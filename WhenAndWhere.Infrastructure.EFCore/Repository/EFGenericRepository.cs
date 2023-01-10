using System.Collections;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.EFCore.UnitOfWork;
using WhenAndWhere.Infrastructure.Repository;
using WhenAndWhere.Infrastructure.UnitOfWork;

namespace WhenAndWhere.Infrastructure.EFCore.Repository;

public class EFGenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    internal readonly EFUnitOfWork Uow;

    public EFGenericRepository(IUnitOfWork uow)
    {
        this.Uow = (EFUnitOfWork) uow;
    }

    public virtual async Task<TEntity?> GetById(params object?[]? keyValues)
    {
        return await Uow.Context.Set<TEntity>().FindAsync(keyValues);
    }

    public virtual Task<List<TEntity>> GetAll()
    {
        return Uow.Context.Set<TEntity>().ToListAsync();
    }

    public virtual void Insert(TEntity entity)
    {
        Uow.Context.Set<TEntity>().Add(entity);
    }
    
    public virtual void Delete(params object?[]? keyValues)
    {
        var entityToDelete = Uow.Context.Set<TEntity>().Find(keyValues);
        if (entityToDelete != null)
        {
            Delete(entityToDelete);
        }
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        var entity = FindAsync(entityToDelete);
        Uow.Context.Remove(entity!);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        var entity = FindAsync(entityToUpdate);
        Uow.Context.Entry(entity!).CurrentValues.SetValues(entityToUpdate);
        Uow.Context.Update(entity!);
    }

    private TEntity? FindAsync(TEntity entity)
    {
        if (Uow.Context.Entry(entity).State != EntityState.Detached) return entity;
        
        IEnumerable<object?> idProperties;
        if (entity is IEntity iEntity)
        {
            idProperties = new List<object?> { iEntity.Id };
        }
        else
        {
            idProperties = entity.GetType().GetProperties()
                .Where(pi => pi.Name.Contains("Id"))
                .Select(pi => pi.GetValue(entity));
        }
        return Uow.Context.Set<TEntity>().Find(idProperties.ToArray());
    }

    public virtual async Task Save()
    {
        await Uow.CommitAsync();
    }
}