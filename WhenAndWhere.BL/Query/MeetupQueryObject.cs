using System;
using AutoMapper;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.Query;

namespace WhenAndWhere.BL.Query;

public class MeetupQueryObject : QueryObjectGeneric<MeetupDTO, Meetup>
{
    public MeetupQueryObject(IMapper mapper, IQuery<Meetup> myQuery) : base(mapper, myQuery)
    {
    }
}

