using AutoMapper;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Filter;
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
    
    public RoleDTO? GetByName(int meetupId, string name)
    {
        var query = new QueryFilterDto<RoleDTO>
        {
            Values = new RoleDTO { MeetupId = meetupId, Name = name },
            WhereColumns = new List<string> { "MeetupId", "Name" }
        };
        return ExecuteQuery(query).Items.FirstOrDefault();
    }

    public List<RoleDTO> GetAllInMeetup(int meetupId)
    {
        var query = new QueryFilterDto<RoleDTO>
        {
            Values = new RoleDTO { MeetupId = meetupId },
            WhereColumns = new List<string> { "MeetupId" }
        };
        return ExecuteQuery(query).Items.ToList();
    }

    public async Task<MeetupDTO> GetMeetup(params object?[]? keyValues)
    {
        return await GetProperty<MeetupDTO>("Meetup", keyValues);
    }

    public async Task<List<UserRoleDTO>> GetAssignedUsers(params object?[]? keyValues)
    {
        return await GetProperty<List<UserRoleDTO>>("AssignedUsers", keyValues);
    }
}