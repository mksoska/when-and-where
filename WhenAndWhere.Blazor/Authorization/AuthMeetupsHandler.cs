﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Services;

namespace WhenAndWhere.Blazor.Authorization;

public class AuthMeetupsHandler : AuthorizationHandler<OperationAuthorizationRequirement, MeetupDTO>
{
    private readonly UserService _userService;
    public AuthMeetupsHandler(UserService userService, MeetupService meetupService)
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