using Microsoft.EntityFrameworkCore;
using WhenAndWhere.Infrastructure.EFCore.UnitOfWork;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.Infrastructure.EFCore.Repository;

public class EFGenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    internal EFUnitOfWork uow;

    public EFGenericRepository(EFUnitOfWork uow)
    {
        this.uow = uow;
    }

    public virtual async Task<TEntity> GetById(object id)
    {
        return await uow.Context.Set<TEntity>().FindAsync(id);
    }

    public virtual async Task<TEntity> GetById(object firstId, object secondId)
    {
        return await uow.Context.Set<TEntity>().FindAsync(firstId, secondId);
    }

    public virtual Task<List<TEntity>> GetAll()
    {
        return uow.Context.Set<TEntity>().ToListAsync();
    }

    public virtual void Insert(TEntity entity)
    {
        uow.Context.Set<TEntity>().Add(entity);
    }

    public virtual void Delete(object id)
    {
        var entityToDelete = uow.Context.Set<TEntity>().Find(id);
        Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (uow.Context.Entry(entityToDelete).State == EntityState.Detached)
        {
            uow.Context.Attach(entityToDelete);
        }
        uow.Context.Remove(entityToDelete);
        uow.Context.Entry(entityToDelete).State = EntityState.Deleted;
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        uow.Context.Set<TEntity>().Attach(entityToUpdate);
        uow.Context.Entry(entityToUpdate).State = EntityState.Modified;
    }

    public virtual async Task Save()
    {
        await uow.CommitAsync();
    }
}