using System;
using WhenAndWhere.BL.Services;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.BL.Facades;

public class MeetupFacade
{
    private readonly MeetupService meetupService;
    private readonly UserService userService;

    public MeetupFacade()
	{

	}

    public async Task CreateMeetupUserInvitation(int meetupId, int userId)
    {
    }
}

