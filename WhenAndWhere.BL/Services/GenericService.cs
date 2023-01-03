using AutoMapper;
using WhenAndWhere.BL.Interfaces;
using WhenAndWhere.Infrastructure.Repository;
using Ardalis.GuardClauses;
using WhenAndWhere.BL.Filter;
using WhenAndWhere.BL.Query;
using WhenAndWhere.Infrastructure.Query;

namespace WhenAndWhere.BL.Services;

// Make abstract and change methods from public to protected?
// Or use dependency injection instead of inheritance?
// Or leave as it is?
public class GenericService<TDto, TEntity> : IGenericService<TDto, TEntity> where TDto : class where TEntity : class, new()
{
    private readonly IMapper _mapper;
    private readonly IRepository<TEntity> _repository;
    private readonly QueryObjectGeneric<TDto, TEntity> _queryObject;

    public GenericService(IRepository<TEntity> repository, IMapper mapper,
        QueryObjectGeneric<TDto, TEntity> queryObject)
    {
        _repository = repository;
        _mapper = mapper;
        _queryObject = queryObject;
    }
    
    public async Task<List<TDto>> GetAll()
    {
        var entities = await _repository.GetAll();
        return _mapper.Map<List<TDto>>(entities);
    }

    protected async Task<TPropertyDto> GetProperty<TPropertyDto>(string property, params object?[]? keyValues)
    {
        var entity = await _repository.GetById(keyValues);
        Guard.Against.Null(entity);
        var propertyValue = entity.GetType().GetProperty(property).GetValue(entity);
        return _mapper.Map<TPropertyDto>(propertyValue);
    }
    
    public QueryResultDto<TDto> ExecuteQuery(QueryFilterDto<TDto> filterDto)
    {
        return _queryObject.ExecuteQuery(filterDto);
    }

    public async Task<TDto?> GetById(params object?[]? keyValues)
    {
        var entity = await _repository.GetById(keyValues);
        return _mapper.Map<TDto?>(entity);
    }

    public virtual async Task<TDto> Create(TDto entityDto)
    {
        var entity = _mapper.Map<TEntity>(entityDto);
        _repository.Insert(entity);
        await _repository.Save();
        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task Update(TDto entityDto)
    {
        var entity = _mapper.Map<TEntity>(entityDto);
        _repository.Update(entity);
        await _repository.Save();
    }

    public virtual async Task Delete(params object?[]? keyValues)
    {
        _repository.Delete(keyValues);
        await _repository.Save();
    }

    public virtual async Task Delete(TDto entityDto)
    {
        var entity = _mapper.Map<TEntity>(entityDto);
        _repository.Delete(entity);
        await _repository.Save();
    }
}