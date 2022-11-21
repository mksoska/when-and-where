using System;
using AutoMapper;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.BL.Services;

public class UserRoleService : GenericService<UserRoleDTO, UserRole>
{
    public UserRoleService(IRepository<UserRole> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
