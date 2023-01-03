using WhenAndWhere.BL.Filter;

namespace WhenAndWhere.BL.Interfaces;

public interface IGenericService<TDto, TEntity> 
    where TDto : class  // TEntity is not used, but needed for Autofac to properly register component
{
    Task<List<TDto>> GetAll();

    Task<TDto?> GetById(params object?[]? keyValues);

    Task<TDto> Create(TDto addressDto);

    Task Update(TDto addressDto);
    
    Task Delete(params object?[]? keyValues);

    QueryResultDto<TDto> ExecuteQuery(QueryFilterDto<TDto> filterDto);
}