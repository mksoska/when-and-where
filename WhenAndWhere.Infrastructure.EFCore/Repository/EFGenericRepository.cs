using Microsoft.EntityFrameworkCore;
using WhenAndWhere.DAL;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.Infrastructure.EFCore.Repository;

public class EFGenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    internal WhenAndWhereDBContext context;
    internal DbSet<TEntity> dbSet;

    public EFGenericRepository(WhenAndWhereDBContext dbcontext)
    {
        context = dbcontext;
        dbSet = context.Set<TEntity>();
    }

    public virtual TEntity GetById(object id)
    {
        return dbSet.Find(id);
    }

    public virtual TEntity GetById(object firstId, object secondId)
    {
        return dbSet.Find(firstId, secondId);
    }

    public virtual List<TEntity> GetAll()
    {
        return dbSet.ToList();
    }

    public virtual void Insert(TEntity entity)
    {
        dbSet.Add(entity);
    }

    public virtual void Delete(object id)
    {
        TEntity entityToDelete = dbSet.Find(id);
        Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (context.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }
        dbSet.Remove(entityToDelete);
        context.Entry(entityToDelete).State = EntityState.Deleted;
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        dbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
    }

    public virtual async Task Save()
    {
        await context.SaveChangesAsync();
    }
}