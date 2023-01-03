using System;
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

    public async Task<MeetupDTO> GetMeetup(params object?[]? keyValues)
    {
        return await GetProperty<MeetupDTO>("Meetup", keyValues);
    }
    
    public async Task<UserDTO> GetOwner(params object?[]? keyValues)
    {
        return await GetProperty<UserDTO>("Owner", keyValues);
    }

    public async Task<List<UserOptionDTO>> GetVoters(params object?[]? keyValues)
    {
        return await GetProperty<List<UserOptionDTO>>("Voters", keyValues);
    }
}

