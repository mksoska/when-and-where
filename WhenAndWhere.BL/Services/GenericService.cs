using AutoMapper;
using WhenAndWhere.BL.Interfaces;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.BL.Services;

public class GenericService<TDto, TEntity> : IGenericService<TDto, TEntity> where TDto : class where TEntity : class
{
    private readonly IMapper _mapper;
    private readonly IRepository<TEntity> _repository;

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

    public void Create(TDto addressDto)
    {
        var address = _mapper.Map<TEntity>(addressDto);
        _repository.Insert(address);
    }

    public void Update(TDto addressDto)
    {
        var address = _mapper.Map<TEntity>(addressDto);
        _repository.Update(address);
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }
}