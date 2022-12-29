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

    public async Task<bool> IsAnyOptionVoted(int userId, int meetupId)
    {
        var meetupOptions = await _meetupService.GetOptions(meetupId);

        foreach (var option in meetupOptions)
        {
            if (_userOptionService.GetById(userId, option.Id) != null)
            {
                return true;
            }
        }

        return false;
    }
}

