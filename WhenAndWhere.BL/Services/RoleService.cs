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

    public async Task<List<UserRoleDTO>> GetAssignedUsers(int id)
    {
        return await GetProperty<List<UserRoleDTO>>(id, "AssignedUsers");
    }
}