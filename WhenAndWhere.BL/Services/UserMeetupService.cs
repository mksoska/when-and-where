using System;
using AutoMapper;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.Repository;
using WhenAndWhere.DAL.Enums;
using Ardalis.GuardClauses;


namespace WhenAndWhere.BL.Services;

public class UserMeetupService : GenericService<UserMeetupDTO, UserMeetup>
{
    public UserMeetupService(IRepository<UserMeetup> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    //public async Task CreateInvitation(UserMeetupDTO userMeetup)
    //{
    //    await Create(userMeetup);
    //}

    //public async Task RemoveInvitation(int userId, int meetupId)
    //{
    //    await Delete(userId, meetupId);
    //}

    public async Task AcceptInvitation(int userId, int meetupId)
    { 
        await UpdateState(userId, meetupId, StateEnum.Accepted);
    }

    public async Task DeclineInvitation(int userId, int meetupId)
    {
        await UpdateState(userId, meetupId, StateEnum.Declined);
    }

    private async Task UpdateState(int userId, int meetupId, StateEnum newState)
    {
        var userMeetup = await GetById(userId, meetupId);
        Guard.Against.Null(userMeetup);
        userMeetup.State = newState;
        await Update(userMeetup);
    }
}

