namespace WhenAndWhere.BL.Interfaces;

public interface IGenericService<TDto, TEntity> 
    where TDto : class  // TEntity is not used, but needed for Autofac to properly register component
{
    Task<List<TDto>> GetAll();

    Task<TDto> GetById(int id);

    void Create(TDto addressDto);

    void Update(TDto addressDto);
    
    void Delete(int id);
}