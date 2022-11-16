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

    public async virtual Task<TEntity> GetById(object id)
    {
        return await dbSet.FindAsync(id);
    }

    public async virtual Task<TEntity> GetById(object firstId, object secondId)
    {
        return await dbSet.FindAsync(firstId, secondId);
    }

    public virtual Task<List<TEntity>> GetAll()
    {
        return dbSet.ToListAsync();
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