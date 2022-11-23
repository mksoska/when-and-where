using AutoMapper;
using WhenAndWhere.BL.Query;
using WhenAndWhere.DAL.Enums;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.BL.Services;

public class RoleService : GenericService<RoleDTO, Role>
{
    public RoleService(IRepository<Role> repository, IMapper mapper, 
        QueryObjectGeneric<RoleDTO, Role> queryObject) : base(repository, mapper, queryObject)
    {
    }

    public async Task<List<UserRoleDTO>> GetAssignedUsers(int id)
    {
        return await GetProperty<List<UserRoleDTO>>(id, "AssignedUsers");
    }
}