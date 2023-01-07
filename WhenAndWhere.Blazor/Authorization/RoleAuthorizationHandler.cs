using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Services;
using WhenAndWhere.DAL.Models;
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
        var meetup = await _meetupService.GetById(meetupId);
        if (meetup == null)
        {
            return;
        }
        if (ReferenceEquals(requirement, Roles.Vote) && IsVotingEnd(meetup))
        {
            return;
        }

        foreach (var roleName in requirement.RoleNames)
        {
            ////////// Handle roles which are not present in AspNetRoles table //////////////////
            if (roleName == Roles.Owner)
            {
                if (IsOwner(user, meetup))
                {
                    context.Succeed(requirement);
                    return;
                }
                continue;
            }

            if (roleName == Roles.Participant)
            {
                if (await IsParticipant(user, meetup))
                {
                    context.Succeed(requirement);
                    return;
                }
                continue;
            }
            /////////////////////////////////////////////////////////////////////////////////////
            
            var role = _roleService.GetByName(meetupId, roleName);
            if (role == null)
            {
                continue;
            }

            if (await _userRoleService.GetById(user.Id, role.Id) != null)
            {
                context.Succeed(requirement);
                return;
            }
        }
    }
    
    private bool IsOwner(UserDTO user, MeetupDTO meetup)
    {
        return user.Id == meetup.OwnerId;
    }

    private async Task<bool> IsParticipant(UserDTO user, MeetupDTO meetup)
    {
        return await _userMeetupService.GetById(user.Id, meetup.Id) != null;
    }

    private bool IsVotingEnd(MeetupDTO meetup)
    {
        return DateTime.Now >= meetup.VotingEnd;
    }
}