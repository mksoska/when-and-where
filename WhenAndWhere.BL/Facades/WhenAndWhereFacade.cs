using System;
using WhenAndWhere.BL.Services;

namespace WhenAndWhere.BL.Facades;

public class WhenAndWhereFacade
{
    private readonly UserService userService;
    private readonly UserMeetupService userMeetupService;
    private readonly MeetupService meetupService;
    private readonly UserOptionService userOptionService;
    private readonly OptionService optionService;
    private readonly AddressService addressService;
    private readonly UserRoleService userRoleService;
    private readonly RoleService roleService;

    public WhenAndWhereFacade()
	{
	}
}

