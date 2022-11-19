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

    public async Task<TPropertyDto> GetProperty<TPropertyDto>(int id, string property)
    {
        var entity = await _repository.GetById(id);
        Guard.Against.Null(entity);
        var propertyValue = entity.GetType().GetProperty(property).GetValue(entity);
        return _mapper.Map<TPropertyDto>(propertyValue);
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