﻿using System;
using AutoMapper;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.Repository;
using Ardalis.GuardClauses;
using WhenAndWhere.BL.Query;
using WhenAndWhere.DTO.Filter;

namespace WhenAndWhere.BL.Services;

public class MeetupService : GenericService<MeetupDTO, Meetup>
{
    public MeetupService(IRepository<Meetup> repository, IMapper mapper, 
	    QueryObjectGeneric<MeetupDTO, Meetup> queryObject) : base(repository, mapper, queryObject)
	{
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

