﻿using System;
using AutoMapper;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.DTO.Filter;

namespace WhenAndWhere.Infrastructure.EFCore
{
    // Configuration for AutoMapper
    public class EFCoreProfile : Profile
    {
        public EFCoreProfile()
        {
            CreateMap<AddressDTO, Address>().ReverseMap();
            CreateMap<MeetupDTO, Meetup>().ReverseMap();
            CreateMap<OptionDTO, Option>().ReverseMap();
            CreateMap<RoleDTO, Role>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<QueryResultDto<UserDTO>, IEnumerable<User>>().ReverseMap();
            CreateMap<UserMeetupDTO, UserMeetup>().ReverseMap();
            CreateMap<UserOptionDTO, UserOption>().ReverseMap();
            CreateMap<UserRoleDTO, UserRole>().ReverseMap();
        }
    }
}

