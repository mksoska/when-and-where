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
        var role = await _repository.GetById(id);
        return _mapper.Map<List<UserDTO>>(role.AssignedUsers);
    }

    public async Task<List<RoleDTO>> GetRoleByName(RoleEnum name)
    {
        var roles = await _repository.GetAll();
        var filtered = roles.Where(role => role.RoleName == name);

        return _mapper.Map<List<RoleDTO>>(filtered);
    }
}