namespace WhenAndWhere.Infrastructure.Repository;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetById(params object?[]? keyValues);
    
    Task<List<TEntity>> GetAll();

    void Insert(TEntity entity);

    void Delete(params object?[]? keyValues);
    
    void Delete(TEntity entityToDelete);

    void Update(TEntity entityToUpdate);

    Task Save();
}