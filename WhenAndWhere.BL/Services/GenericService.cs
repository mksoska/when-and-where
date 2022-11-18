using AutoMapper;
using WhenAndWhere.BL.Interfaces;
using WhenAndWhere.Infrastructure.Repository;

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
        var addresses = await _repository.GetAll();
        return _mapper.Map<List<TDto>>(addresses);
    }

    public async Task<TDto> GetById(int id)
    {
        var address = await _repository.GetById(id);
        return _mapper.Map<TDto>(address);
    }

    public async Task Create(TDto addressDto)
    {
        var address = _mapper.Map<TEntity>(addressDto);
        _repository.Insert(address);
        await _repository.Save();
    }

    public async Task Update(TDto addressDto)
    {
        var address = _mapper.Map<TEntity>(addressDto);
        _repository.Update(address);
        await _repository.Save();
    }

    public async Task Delete(int id)
    {
        _repository.Delete(id);
        await _repository.Save();
    }
}