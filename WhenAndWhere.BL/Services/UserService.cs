using System;
using AutoMapper;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.Repository;
using Ardalis.GuardClauses;

namespace WhenAndWhere.BL.Services;

public class UserService : GenericService<UserDTO, User>
{
    public UserService(IRepository<User> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    public async Task<List<MeetupDTO>> GetUserJoinedMeetups(int id)
    {
        var joinedMeetups = await GetProperty<List<UserMeetupDTO>>(id, "JoinedMeetups");
        return joinedMeetups.Select(um => um.Meetup).ToList();
    }

    public async Task<List<MeetupDTO>> GetUserOwnedMeetups(int id)
    {
        return await GetProperty<List<MeetupDTO>>(id, "OwnedMeetups");
    }

    public async Task<List<OptionDTO>> GetUserCreatedOptions(int id)
    {
        return await GetProperty<List<OptionDTO>>(id, "CreatedOptions");
    }

    public async Task<List<OptionDTO>> GetUserVotedOptions(int id)
    {
        var votedOptions = await GetProperty<List<UserOptionDTO>>(id, "VotedOptions");
        return votedOptions.Select(uo => uo.Option).ToList();
    }

    public async Task<List<RoleDTO>> GetUserAssignedRoles(int id)
    {
        var assignedRoles = await GetProperty<List<UserRoleDTO>>(id, "AssignedRoles");
        return assignedRoles.Select(ur => ur.Role).ToList();
    }
}

