using System;
using AutoMapper;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.BL.Services;

public class OptionService : GenericService<OptionDTO, Option>
{
    public OptionService(IRepository<Option> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    public async Task<List<UserOptionDTO>> GetVoters(int id)
    {
        return await GetProperty<List<UserOptionDTO>>(id, "Voters");
    }
}

