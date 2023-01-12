using AutoMapper;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Query;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.BL.Services;

public class MeetupService : GenericService<MeetupDTO, Meetup>
{
    public MeetupService(IRepository<Meetup> repository, IMapper mapper,
        QueryObjectGeneric<MeetupDTO, Meetup> queryObject) : base(repository, mapper, queryObject) { }
    
    public async Task<UserDTO> GetOwner(params object?[]? keyValues)
    {
        return await GetProperty<UserDTO>("Owner", keyValues);
    }

    public async Task<List<OptionDTO>> GetOptions(params object?[]? keyValues)
    {
        return await GetProperty<List<OptionDTO>>("Options", keyValues);
    }

    public async Task<List<UserMeetupDTO>> GetInvitedUsers(params object?[]? keyValues)
    {
        return await GetProperty<List<UserMeetupDTO>>("InvitedUsers", keyValues);
    }

    public async Task<List<RoleDTO>> GetRoles(params object?[]? keyValues)
    {
        return await GetProperty<List<RoleDTO>>("Roles", keyValues);
    }
}
