using WhenAndWhere.DAL;
using WhenAndWhere.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WhenAndWhere.Infrastructure.EFCore.Repository
{
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
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
