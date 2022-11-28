using System;
using WhenAndWhere.BL.Services;
using WhenAndWhere.DTO;
using System.Linq;

namespace WhenAndWhere.BL.Facades;

public class AdministratorFacade
{
    private readonly UserService _userService;
    private readonly UserMeetupService _userMeetupService;
    private readonly MeetupService _meetupService;
    private readonly UserOptionService _userOptionService;
    private readonly OptionService _optionService;
    private readonly AddressService _addressService;
    private readonly UserRoleService _userRoleService;
    private readonly RoleService _roleService;

    public AdministratorFacade(
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

    public async Task<List<MeetupDTO>> GetAllMeetupsWhereAdmin(int userId)
    {
        var result = new List<MeetupDTO>();

        foreach (var userMeetup in await _userService.GetInvitedMeetups(userId))
        {
            var roles = await _meetupService.GetRoles(userMeetup.MeetupId);
            if (roles.Where(r => _userRoleService.GetById(userId, r.Id) != null).Any())
            {
                result.Add(await _meetupService.GetById(userMeetup.MeetupId));
            }
        }
        return result;
    }
}

