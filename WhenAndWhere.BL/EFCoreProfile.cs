using AutoMapper;
using WhenAndWhere.BL;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.DAL.Models;


namespace WhenAndWhere.BL
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
            CreateMap<UserMeetupDTO, UserMeetup>().ReverseMap();
            CreateMap<UserOptionDTO, UserOption>().ReverseMap();
            CreateMap<UserRoleDTO, UserRole>().ReverseMap();
        }
    }
}

