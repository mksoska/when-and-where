using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Services;
using RouteData = Microsoft.AspNetCore.Components.RouteData;

namespace WhenAndWhere.Blazor.Authorization;

public class RoleAuthorizationHandler
{
    private readonly UserService _userService;
    private readonly UserRoleService _userRoleService;
    private readonly RoleService _roleService;
    private readonly UserMeetupService _userMeetupService;
    private readonly MeetupService _meetupService;
    
    public RoleAuthorizationHandler(UserService userService, UserRoleService userRoleService, RoleService roleService, UserMeetupService userMeetupService, MeetupService meetupService)
    {
        _userService = userService;
        _userRoleService = userRoleService;
        _roleService = roleService;
        _userMeetupService = userMeetupService;
        _meetupService = meetupService;
    }

    public async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, RoleAuthorizationRequirement requirement, int meetupId)
    {
        var user = _userService.GetByName(context.User.Identity?.Name)!;

        foreach (var roleName in requirement.RoleNames)
        {
            if (roleName == "Owner" && await IsOwner(user, meetupId))
            {
                context.Succeed(requirement);
                return;
            }

            if (roleName == "User" && await IsUser(user, meetupId))
            {
                context.Succeed(requirement);
                return;
            }
            

            var role = _roleService.GetByName(meetupId, roleName);
            if (role == null)
            {
                return;
            }

            if (await _userRoleService.GetById(user.Id, role.Id) != null)
            {
                context.Succeed(requirement);
                return;
            }
        }
    }
    
    private async Task<bool> IsOwner(UserDTO user, int meetupId)
    {
        var meetup = await _meetupService.GetById(meetupId);
        return meetup != null && user.Id == meetup.OwnerId;
    }

    private async Task<bool> IsUser(UserDTO user, int meetupId)
    {
        return await _userMeetupService.GetById(user.Id, meetupId) != null;
    }
}