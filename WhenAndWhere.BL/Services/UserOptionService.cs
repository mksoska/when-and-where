using System;
using AutoMapper;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Query;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.BL.Services;

public class UserOptionService : GenericService<UserOptionDTO, UserOption>
{
    public UserOptionService(IRepository<UserOption> repository, IMapper mapper, 
        QueryObjectGeneric<UserOptionDTO, UserOption> queryObject) : base(repository, mapper, queryObject)
    {
    }
}

