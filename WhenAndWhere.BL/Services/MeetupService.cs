using System;
using AutoMapper;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.Repository;
using Ardalis.GuardClauses;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Interfaces;
using WhenAndWhere.BL.Query;

namespace WhenAndWhere.BL.Services;

public class MeetupService : GenericService<MeetupDTO, Meetup>
{
    public MeetupService(IRepository<Meetup> repository, IMapper mapper, 
	    QueryObjectGeneric<MeetupDTO, Meetup> queryObject, IBatchService batchService) : base(repository, mapper, queryObject)
	{
	}

    public override async Task Create(MeetupDTO meetupDto)
    {
	    
    }

    public async Task<UserDTO> GetOwner(int id)
    {
	    return await GetProperty<UserDTO>(id, "Owner");
    }

    public async Task<List<OptionDTO>> GetOptions(int id)
	{
        return await GetProperty<List<OptionDTO>>(id, "Options");
    }

    public async Task<List<UserMeetupDTO>> GetInvitedUsers(int id)
	{
        return await GetProperty<List<UserMeetupDTO>>(id, "InvitedUsers");
    }

    public async Task<List<RoleDTO>> GetRoles(int id)
	{
		return await GetProperty<List<RoleDTO>>(id, "Roles");
	}
}

