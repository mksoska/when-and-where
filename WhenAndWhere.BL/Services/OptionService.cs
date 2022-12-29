﻿using System;
using AutoMapper;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Query;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.BL.Services;

public class OptionService : GenericService<OptionDTO, Option>
{
    public OptionService(IRepository<Option> repository, IMapper mapper, 
        QueryObjectGeneric<OptionDTO, Option> queryObject) : base(repository, mapper, queryObject)
    {
    }

    public async Task<MeetupDTO> GetMeetup(int id)
    {
        return await GetProperty<MeetupDTO>(id, "Meetup");
    }
    
    public async Task<UserDTO> GetOwner(int id)
    {
        return await GetProperty<UserDTO>(id, "Owner");
    }

    public async Task<List<UserOptionDTO>> GetVoters(int id)
    {
        return await GetProperty<List<UserOptionDTO>>(id, "Voters");
    }
}

