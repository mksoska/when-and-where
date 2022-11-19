using System;
using AutoMapper;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.Repository;
using Ardalis.GuardClauses;

namespace WhenAndWhere.BL.Services;

public class MeetupService : GenericService<MeetupDTO, Meetup>
{
    public MeetupService(IRepository<Meetup> repository, IMapper mapper) : base(repository, mapper)
	{
	}

	public async Task<List<OptionDTO>> GetMeetupOptions(int id)
	{
        return await GetProperty<List<OptionDTO>>(id, "Options");
    }

    public async Task<List<UserDTO>> GetMeetupJoinedUsers(int id)
	{
        var joinedUsers = await GetProperty<List<UserMeetupDTO>>(id, "JoinedUsers");
		return joinedUsers.Select(um => um.User).ToList();
    }

    public async Task<List<RoleDTO>> GetMeetupRoles(int id)
	{
		return await GetProperty<List<RoleDTO>>(id, "Roles");
	}
}

