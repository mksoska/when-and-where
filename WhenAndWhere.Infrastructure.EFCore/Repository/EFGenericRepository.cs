﻿using Microsoft.EntityFrameworkCore;
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

    public virtual async Task<TEntity?> GetById(object id)
    {
        return await Uow.Context.Set<TEntity>().FindAsync(id);
    }

    public virtual async Task<TEntity?> GetById(object firstId, object secondId)
    {
        return await Uow.Context.Set<TEntity>().FindAsync(firstId, secondId);
    }

    public virtual Task<List<TEntity>> GetAll()
    {
        return Uow.Context.Set<TEntity>().ToListAsync();
    }

    public virtual void Insert(TEntity entity)
    {
        Uow.Context.Set<TEntity>().Add(entity);
    }

    public virtual void Delete(object id)
    {
        var entityToDelete = Uow.Context.Set<TEntity>().Find(id);
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
        Uow.Context.Set<TEntity>().Attach(entityToUpdate);
        Uow.Context.Entry(entityToUpdate).State = EntityState.Modified;
    }

    public virtual async Task Save()
    {
        await Uow.CommitAsync();
    }
}