using System;
using AutoMapper;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.Repository;
using Ardalis.GuardClauses;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Filter;
using WhenAndWhere.BL.Query;

namespace WhenAndWhere.BL.Services;

public class UserService : GenericService<UserDTO, User>
{
    public UserService(IRepository<User> repository, IMapper mapper, 
        QueryObjectGeneric<UserDTO, User> queryObject) : base(repository, mapper, queryObject)
    {
    }

    public UserDTO? GetByName(string? username)
    {
        if (string.IsNullOrEmpty(username))
        {
            return null;
        }
        var userFilter = new UserDTO { UserName = username };
        var query = new QueryFilterDto<UserDTO>
        {
            Values = userFilter,
            WhereColumns = new List<string> { "UserName" }
        };
        return ExecuteQuery(query).Items.FirstOrDefault();
    }

    public async Task<List<UserMeetupDTO>> GetInvitedMeetups(params object?[]? keyValues)
    {
        return await GetProperty<List<UserMeetupDTO>>("InvitedMeetups", keyValues);
    }

    public async Task<List<MeetupDTO>> GetOwnedMeetups(params object?[]? keyValues)
    {
        return await GetProperty<List<MeetupDTO>>("OwnedMeetups", keyValues);
    }

    public async Task<List<OptionDTO>> GetCreatedOptions(params object?[]? keyValues)
    {
        return await GetProperty<List<OptionDTO>>("CreatedOptions", keyValues);
    }

    public async Task<List<UserOptionDTO>> GetVotedOptions(params object?[]? keyValues)
    {
        return await GetProperty<List<UserOptionDTO>>("VotedOptions", keyValues);
    }

    public async Task<List<UserRoleDTO>> GetAssignedRoles(params object?[]? keyValues)
    {
        return await GetProperty<List<UserRoleDTO>>("AssignedRoles", keyValues);
    }
}

