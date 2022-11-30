using AutoMapper;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Query;
using WhenAndWhere.DAL.Enums;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.BL.Services;

public class RoleService : GenericService<RoleDTO, Role>
{
    public RoleService(IRepository<Role> repository, IMapper mapper, 
        QueryObjectGeneric<RoleDTO, Role> queryObject) : base(repository, mapper, queryObject)
    {
    }

    public async Task<MeetupDTO> GetMeetup(int id)
    {
        return await GetProperty<MeetupDTO>(id, "Meetup");
    }

    public async Task<List<UserRoleDTO>> GetAssignedUsers(int id)
    {
        return await GetProperty<List<UserRoleDTO>>(id, "AssignedUsers");
    }
}