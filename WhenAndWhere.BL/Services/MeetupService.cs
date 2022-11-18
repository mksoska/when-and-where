using System;
using AutoMapper;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.BL.Services;

public class MeetupService : GenericService<MeetupDTO, Meetup>
{
    public MeetupService(IRepository<Meetup> repository, IMapper mapper) : base(repository, mapper)
	{
	}

	public async Task<List<OptionDTO>> GetMeetupOptions(int id)
	{
		var meetup = await _repository.GetById(id);
		return _mapper.Map<List<OptionDTO>>(meetup!.Options);
	}

	public async Task<List<UserProfileDTO>> GetMeetupJoinedUsers(int id)
	{
        var meetup = await _repository.GetById(id);
		var joinedUsers = meetup!.JoinedUsers.Select(u => u.User).ToList();
        return _mapper.Map<List<UserProfileDTO>>(joinedUsers);
    }
}

