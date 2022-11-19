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
		var meetup = await _repository.GetById(id);
        Guard.Against.Null(meetup);
        return _mapper.Map<List<OptionDTO>>(meetup.Options);
	}

	public async Task<List<UserDTO>> GetMeetupJoinedUsers(int id)
	{
		return await GetPropertyManyToMany<UserDTO>(id, "JoinedUsers", "User");
    }

	public async Task<List<RoleDTO>> GetMeetupRoles(int id)
	{
		return await GetPropertyOnetoMany<RoleDTO>(id, "Roles");
		//var meetup = await _repository.GetById(id);
  //      Guard.Against.Null(meetup);
  //      return _mapper.Map<List<RoleDTO>>(meetup.Roles);
	}
}

