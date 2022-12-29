using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Services;
using RouteData = Microsoft.AspNetCore.Components.RouteData;

namespace WhenAndWhere.Blazor.Authorization;

public class PolicyAuthorizationHandler : IAuthorizationHandler
{
    private readonly UserService _userService;
    private readonly UserRoleService _userRoleService;
    private readonly RoleService _roleService;
    private readonly UserMeetupService _userMeetupService;
    private readonly MeetupService _meetupService;

    public PolicyAuthorizationHandler(UserService userService, UserRoleService userRoleService, RoleService roleService, UserMeetupService userMeetupService, MeetupService meetupService)
    {
        _userService = userService;
        _userRoleService = userRoleService;
        _roleService = roleService;
        _userMeetupService = userMeetupService;
        _meetupService = meetupService;
    }

    private void Succeed(AuthorizationHandlerContext context, List<IAuthorizationRequirement> requirements)
    {
        requirements.ForEach(context.Succeed);
    }

    private async Task HandleRolesAsync(AuthorizationHandlerContext context, UserDTO user, int meetupId)
    {
        var roleRequirements = context.PendingRequirements.Where(r => r is RoleAuthorizationRequirement).ToList();
        foreach (var requirement in roleRequirements)
        {
            if (requirement == Roles.Owner && await IsOwner(user, meetupId))
            {
                Succeed(context, roleRequirements);
            }

            if (requirement == Roles.User && await IsUser(user, meetupId))
            {
                Succeed(context, roleRequirements);
            }
            
            var roleName = ((RoleAuthorizationRequirement)requirement).RoleName;

            var role = _roleService.GetByName(meetupId, roleName);

            if (role != null && await _userRoleService.GetById(user.Id, role.Id) != null)
            {
                Succeed(context, roleRequirements);
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

    public async Task HandleAsync(AuthorizationHandlerContext context)
    {
        if (context.Resource is not RouteData rd || rd.RouteValues.TryGetValue("meetupId", out var meetupId))
        {
            return;
        }
        
        var user = _userService.GetByName(context.User.Identity?.Name);
        if (user == null)
        {
            return;
        }

        await HandleRolesAsync(context, user, (int)meetupId);
    }
}