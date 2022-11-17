namespace WhenAndWhere.BL.Interfaces;

public interface IGenericService<TDto> where TDto : class
{
    Task<List<TDto>> GetAll();

    Task<TDto> GetById(int id);

    void Create(TDto addressDto);

    void Update(TDto addressDto);
    
    void Delete(int id);
}