using System;
using AutoMapper;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.Repository;
using WhenAndWhere.DAL.Enums;
using Ardalis.GuardClauses;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Filter;
using WhenAndWhere.BL.Query;


namespace WhenAndWhere.BL.Services;

public class UserMeetupService : GenericService<UserMeetupDTO, UserMeetup>
{
    public UserMeetupService(IRepository<UserMeetup> repository, IMapper mapper, 
        QueryObjectGeneric<UserMeetupDTO, UserMeetup> queryObject) : base(repository, mapper, queryObject)
    {
    }
}

