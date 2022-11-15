namespace WhenAndWhere.Infrastructure.Repository;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetById(object id);

    Task<TEntity> GetById(object firstId, object secondId);

    List<TEntity> GetAll();

    void Insert(TEntity entity);

    void Delete(object id);

    void Delete(TEntity entityToDelete);

    void Update(TEntity entityToUpdate);

    Task Save();
}