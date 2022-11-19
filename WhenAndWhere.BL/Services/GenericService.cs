using AutoMapper;
using WhenAndWhere.BL.Interfaces;
using WhenAndWhere.Infrastructure.Repository;
using Ardalis.GuardClauses;

namespace WhenAndWhere.BL.Services;

public class GenericService<TDto, TEntity> : IGenericService<TDto, TEntity> where TDto : class where TEntity : class
{
    protected readonly IMapper _mapper;
    protected readonly IRepository<TEntity> _repository;

    public GenericService(IRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<List<TDto>> GetAll()
    {
        var entities = await _repository.GetAll();
        return _mapper.Map<List<TDto>>(entities);
    }

    public async Task<List<TPropertyDto>> GetPropertyManyToMany<TPropertyDto>(int id, string associationEntity, string property)
    {
        var entity = await _repository.GetById(id);
        Guard.Against.Null(entity);
        var associationEntityList = entity.GetType().GetProperty(associationEntity).GetValue(entity);
        var propertyList = ((IEnumerable<object>)associationEntityList).Select(ae => ae.GetType().GetProperty(property).GetValue(ae));
        return _mapper.Map<List<TPropertyDto>>(propertyList);
    }

    public async Task<List<TPropertyDto>> GetPropertyOnetoMany<TPropertyDto>(int id, string property)
    {
        var entity = await _repository.GetById(id);
        Guard.Against.Null(entity);
        var propertyList = entity.GetType().GetProperty(property).GetValue(entity);
        return _mapper.Map<List<TPropertyDto>>(propertyList);
    }

    public async Task<TDto> GetById(int id)
    {
        var entity = await _repository.GetById(id);
        return _mapper.Map<TDto>(entity);
    }

    public async Task Create(TDto entityDto)
    {
        var entity = _mapper.Map<TEntity>(entityDto);
        _repository.Insert(entity);
        await _repository.Save();
    }

    public async Task Update(TDto entityDto)
    {
        var entity = _mapper.Map<TEntity>(entityDto);
        _repository.Update(entity);
        await _repository.Save();
    }

    public async Task Delete(int id)
    {
        _repository.Delete(id);
        await _repository.Save();
    }
}