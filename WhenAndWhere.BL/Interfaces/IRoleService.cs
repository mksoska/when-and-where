using WhenAndWhere.DTO;

namespace WhenAndWhere.BL.Interfaces;

public interface IRoleService
{
    Task<RoleDTO> GetRole(int id);

    Task CreateRole(RoleDTO roleDto);

    Task UpdateRole(RoleDTO roleDto);

    Task DeleteRole(int id);
}