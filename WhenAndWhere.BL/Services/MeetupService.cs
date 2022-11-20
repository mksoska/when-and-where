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

	public async Task<List<OptionDTO>> GetOptions(int id)
	{
        return await GetProperty<List<OptionDTO>>(id, "Options");
    }

    public async Task<List<UserDTO>> GetJoinedUsers(int id)
	{
        var joinedUsers = await GetProperty<List<UserMeetupDTO>>(id, "JoinedUsers");
		return joinedUsers.Select(um => um.User).ToList();
    }

    public async Task<List<RoleDTO>> GetRoles(int id)
	{
		return await GetProperty<List<RoleDTO>>(id, "Roles");
	}
}

