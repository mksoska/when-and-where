using Microsoft.AspNetCore.Authorization;

namespace WhenAndWhere.Blazor.Authorization;

public class RoleAuthorizationResourceHandler : AuthorizationHandler<RoleAuthorizationRequirement, int>
{
    private readonly RoleAuthorizationHandler _roleAuthorizationHandler;
    
    public RoleAuthorizationResourceHandler(RoleAuthorizationHandler roleAuthorizationHandler) 
    {
        _roleAuthorizationHandler = roleAuthorizationHandler;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, RoleAuthorizationRequirement requirement, int meetupId)
    {
        await _roleAuthorizationHandler.HandleRequirementAsync(context, requirement, meetupId);
    }
}