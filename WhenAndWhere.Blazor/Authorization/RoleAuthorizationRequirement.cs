using Microsoft.AspNetCore.Authorization;

namespace WhenAndWhere.Blazor.Authorization;

public class RoleAuthorizationRequirement : IAuthorizationRequirement
{
    public RoleAuthorizationRequirement(string roleName) => RoleName = roleName;
    
    public string RoleName { get; }
}