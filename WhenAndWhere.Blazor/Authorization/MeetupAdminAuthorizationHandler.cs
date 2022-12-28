using Microsoft.AspNetCore.Authorization;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Services;

namespace WhenAndWhere.Blazor.Authorization;

public class MeetupAdminAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, MeetupDTO>
{
    private readonly UserService _userService;
    private readonly UserRoleService _userRoleService;
    private readonly RoleService _roleService;
    private readonly MeetupService _meetupService;

    public MeetupAdminAuthorizationHandler(UserService userService, UserRoleService userRoleService, RoleService roleService, MeetupService meetupService)
    {
        _userService = userService;
        _userService = userService;
        _roleService = roleService;
        _meetupService = meetupService;
    }
    
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement,
        MeetupDTO resource)
    {
        var user = _userService.GetByName(context.User.Identity?.Name);
        var meetup = _meetupService.GetById(resource.Id);

        if (true)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}