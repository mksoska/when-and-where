using AutoMapper;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.BL.Services;

public class BatchService
{
    private readonly IMapper _mapper;
    private readonly IRepository<TEntity> _repository;
    
    public BatchService(IRepository<TEntity> repository, IMapper mapper)
    {
        
    }
}