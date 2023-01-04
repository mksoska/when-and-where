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
        Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (Uow.Context.Entry(entityToDelete).State == EntityState.Detached)
        {
            Uow.Context.Attach(entityToDelete);
        }
        Uow.Context.Remove(entityToDelete);
        Uow.Context.Entry(entityToDelete).State = EntityState.Deleted;
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        IEnumerable<object?> idProperties;
        if (entityToUpdate is IEntity iEntity)
        {
            idProperties = new List<object?> { iEntity.Id };
        }
        else
        {
            idProperties = entityToUpdate.GetType().GetProperties()
                .Where(pi => pi.Name.Contains("Id"))
                .Select(pi => pi.GetValue(entityToUpdate));
        }

        var entity = Uow.Context.Set<TEntity>().Find(idProperties.ToArray());
        Uow.Context.Entry(entity!).State = EntityState.Detached;
        Uow.Context.Set<TEntity>().Attach(entityToUpdate);
        Uow.Context.Entry(entityToUpdate).State = EntityState.Modified;
    }

    public virtual async Task Save()
    {
        await Uow.CommitAsync();
    }
}