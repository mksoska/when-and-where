using AutoMapper;
using WhenAndWhere.DAL.Enums;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.BL.Services;

public class RoleService : GenericService<RoleDTO, Role>
{
    public RoleService(IRepository<Role> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    public async Task<List<UserDTO>> GetAssignedUsers(int id)
    {
        var assignedUsers = await GetProperty<List<UserRoleDTO>>(id, "AssignedUsers");
        return assignedUsers.Select(ur => ur.User).ToList();
    }

    public async Task<List<RoleDTO>> GetRoleByName(RoleEnum name)
    {
        var roles = await GetAll();
        return roles.Where(role => role.RoleName == name).ToList();
        //Use Query with QueryObject instead
    }
}