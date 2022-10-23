namespace WhenAndWhere.Infrastructure.Repository;

public interface IRepository<TEntity> where TEntity : class
{
    TEntity GetById(object id);

    List<TEntity> GetAll();

    void Insert(TEntity entity);

    void Delete(object id);

    void Delete(TEntity entityToDelete);

    void Update(TEntity entityToUpdate);
}