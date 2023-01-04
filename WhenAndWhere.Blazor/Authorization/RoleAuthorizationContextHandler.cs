using Microsoft.AspNetCore.Authorization;
using WhenAndWhere.BL.Services;
using RouteData = Microsoft.AspNetCore.Components.RouteData;
using Microsoft.AspNetCore.Components;

namespace WhenAndWhere.Blazor.Authorization;

public class RoleAuthorizationContextHandler : AuthorizationHandler<RoleAuthorizationRequirement>
{
    private readonly RoleAuthorizationHandler _roleAuthorizationHandler;
    
    public RoleAuthorizationContextHandler(RoleAuthorizationHandler roleAuthorizationHandler)
    {
        _roleAuthorizationHandler = roleAuthorizationHandler;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, RoleAuthorizationRequirement requirement)
    {
        if (context.Resource is RouteData rd && rd.RouteValues.TryGetValue("meetupId", out var meetupId))
        {
            await _roleAuthorizationHandler.HandleRequirementAsync(context, requirement, (int)meetupId);
        }
    }
}