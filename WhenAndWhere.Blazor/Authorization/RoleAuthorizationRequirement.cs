using Microsoft.AspNetCore.Authorization;

namespace WhenAndWhere.Blazor.Authorization;

public class RoleAuthorizationRequirement : IAuthorizationRequirement
{
    public RoleAuthorizationRequirement(params string[] roleNames) => RoleNames = roleNames;
    
    public string[] RoleNames { get; }
}