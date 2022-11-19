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
        var user = await _repository.GetById(id);
        Guard.Against.Null(user);
        var joinedMeetups = user.JoinedMeetups.Select(um => um.Meetup).ToList();
        return _mapper.Map<List<MeetupDTO>>(joinedMeetups);
    }

    public async Task<List<MeetupDTO>> GetUserOwnedMeetups(int id)
    {
        var user = await _repository.GetById(id);
        Guard.Against.Null(user);
        return _mapper.Map<List<MeetupDTO>>(user.OwnedMeetups);
    }

    public async Task<List<OptionDTO>> GetUserCreatedOptions(int id)
    {
        var user = await _repository.GetById(id);
        Guard.Against.Null(user);
        return _mapper.Map<List<OptionDTO>>(user.CreatedOptions);
    }

    public async Task<List<OptionDTO>> GetUserVotedOptions(int id)
    {
        var user = await _repository.GetById(id);
        Guard.Against.Null(user);
        var votedOptions = user.VotedOptions.Select(uo => uo.Option);
        return _mapper.Map<List<OptionDTO>>(votedOptions);
    }

    public async Task<List<RoleDTO>> GetUserAssignedRoles(int id)
    {
        var user = await _repository.GetById(id);
        Guard.Against.Null(user);
        return _mapper.Map<List<RoleDTO>>(user.AssignedRoles);
    }
}

