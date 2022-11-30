using System;
using AutoMapper;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.Repository;
using Ardalis.GuardClauses;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Query;

namespace WhenAndWhere.BL.Services;

public class UserService : GenericService<UserDTO, User>
{
    public UserService(IRepository<User> repository, IMapper mapper, 
        QueryObjectGeneric<UserDTO, User> queryObject) : base(repository, mapper, queryObject)
    {
    }

    public async Task<List<UserMeetupDTO>> GetInvitedMeetups(int id)
    {
        return await GetProperty<List<UserMeetupDTO>>(id, "InvitedMeetups");
    }

    public async Task<List<MeetupDTO>> GetOwnedMeetups(int id)
    {
        return await GetProperty<List<MeetupDTO>>(id, "OwnedMeetups");
    }

    public async Task<List<OptionDTO>> GetCreatedOptions(int id)
    {
        return await GetProperty<List<OptionDTO>>(id, "CreatedOptions");
    }

    public async Task<List<UserOptionDTO>> GetVotedOptions(int id)
    {
        return await GetProperty<List<UserOptionDTO>>(id, "VotedOptions");
    }

    public async Task<List<UserRoleDTO>> GetAssignedRoles(int id)
    {
        return await GetProperty<List<UserRoleDTO>>(id, "AssignedRoles");
    }
}

