using System;
using AutoMapper;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.Repository;
using WhenAndWhere.DAL.Enums;
using Ardalis.GuardClauses;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Query;


namespace WhenAndWhere.BL.Services;

public class UserMeetupService : GenericService<UserMeetupDTO, UserMeetup>
{
    public UserMeetupService(IRepository<UserMeetup> repository, IMapper mapper, 
        QueryObjectGeneric<UserMeetupDTO, UserMeetup> queryObject) : base(repository, mapper, queryObject)
    {
    }
    
    public async Task SetUserInvitationState(int userId, int meetupId, StateEnum state)
    {
        var invitation = await GetById(userId, meetupId);
        invitation.State = state;
        await Update(invitation);
    }
}

