using System;
using AutoMapper;
using WhenAndWhere.BL.Query;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.BL.Services;

public class UserRoleService : GenericService<UserRoleDTO, UserRole>
{
    public UserRoleService(IRepository<UserRole> repository, IMapper mapper, 
        QueryObjectGeneric<UserRoleDTO, UserRole> queryObject) : base(repository, mapper, queryObject)
    {
    }
}
