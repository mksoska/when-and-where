using Microsoft.AspNetCore.Authorization;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Services;

namespace WhenAndWhere.Blazor.Authorization;

public class MeetupAdminAuthHandler : AuthorizationHandler<OperationAuthorizationRequirement, MeetupDTO>
{
    private readonly UserService _userService;
    private readonly UserRoleService _userRoleService;
    private readonly RoleService _roleService;

    public MeetupAdminAuthHandler(UserService userService, UserRoleService userRoleService, RoleService roleService)
    {
        _userService = userService;
        _userRoleService = userRoleService;
        _roleService = roleService;
    }
    
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement,
        MeetupDTO resource)
    {
        var user = _userService.GetByName(context.User.Identity?.Name);
        var adminRole = await _roleService.GetByName(resource.Id, "Admin");

        if (await _userRoleService.GetById(user.Id, adminRole.Id) != null)
        {
            context.Succeed(requirement);
        }
    }
}