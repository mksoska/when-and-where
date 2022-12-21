using System;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Services;
using WhenAndWhere.DAL.Enums;

namespace WhenAndWhere.BL.Facades;

public class WhenAndWhereFacade
{
    private readonly UserService _userService;
    private readonly UserMeetupService _userMeetupService;
    private readonly MeetupService _meetupService;
    private readonly UserOptionService _userOptionService;
    private readonly OptionService _optionService;
    private readonly AddressService _addressService;
    private readonly UserRoleService _userRoleService;
    private readonly RoleService _roleService;

    public WhenAndWhereFacade(
            UserService userService,
            UserMeetupService userMeetupService,
            MeetupService meetupService,
            UserOptionService userOptionService,
            OptionService optionService,
            AddressService addressService,
            UserRoleService userRoleService,
            RoleService roleService
        )
    {
        _userService = userService;
        _userMeetupService = userMeetupService;
        _meetupService = meetupService;
        _userOptionService = userOptionService;
        _optionService = optionService;
        _addressService = addressService;
        _userRoleService = userRoleService;
        _roleService = roleService;
    }

    public async Task<List<UserRoleDTO>> GetMeetupUserRoles(int meetupId)
    {
        var result = new List<UserRoleDTO>();

        foreach (var role in await _meetupService.GetRoles(meetupId))
        {
            result.AddRange(await _roleService.GetAssignedUsers(role.Id));
            
        }
        return result;
    }

    public async Task<bool> IsUserInRole(int meetupId, string userName, string roleName)
    {
        var userId = _userService.GetByName(userName).Id;
        var roleId = _roleService.GetByName(meetupId, roleName).Id;

        if (await _userRoleService.GetById(userId, roleId) != null)
        {
            return true;
        }
        
        //if ()
        return false;
    }

    public async Task<bool> IsMeetupOwner(int meetupId, string userName)
    {
        var userId = _userService.GetByName(userName).Id;
        var meetup = await _meetupService.GetById(meetupId);
        return userId == meetup.OwnerId;
    }
}

