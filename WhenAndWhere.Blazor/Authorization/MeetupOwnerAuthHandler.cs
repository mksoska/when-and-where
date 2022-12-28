using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Services;

namespace WhenAndWhere.Blazor.Authorization;

public class MeetupOwnerAuthHandler : AuthorizationHandler<OperationAuthorizationRequirement, MeetupDTO>
{
    private readonly UserService _userService;
    public MeetupOwnerAuthHandler(UserService userService)
    {
        _userService = userService;
    }
    
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement,
        MeetupDTO resource)
    {
        var user = _userService.GetByName(context.User.Identity?.Name);

        if (user.Id == resource.OwnerId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}