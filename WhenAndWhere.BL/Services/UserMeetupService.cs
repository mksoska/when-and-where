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
}

