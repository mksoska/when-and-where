using System;
using AutoMapper;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.BL.Services;

public class UserOptionService : GenericService<UserOptionDTO, UserOption>
{
    public UserOptionService(IRepository<UserOption> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}

