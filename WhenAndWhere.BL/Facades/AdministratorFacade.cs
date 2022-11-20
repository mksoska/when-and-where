using System;
using WhenAndWhere.BL.Services;
using WhenAndWhere.DTO;

namespace WhenAndWhere.BL.Facades;

public class AdministratorFacade
{
    private readonly UserService userService;
    private readonly UserMeetupService userMeetupService;
    private readonly MeetupService meetupService;
    private readonly UserOptionService userOptionService;
    private readonly OptionService optionService;
    private readonly AddressService addressService;
    private readonly UserRoleService userRoleService;
    private readonly RoleService roleService;

    public AdministratorFacade()
	{
        //public async Task<List<MeetupDTO>> GetAllMeetupsWhereAdmin(int userId)
        //{

        //}
	}
}

