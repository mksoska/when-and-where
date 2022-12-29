using Microsoft.AspNetCore.Authorization;
using WhenAndWhere.BL.DTOs;

namespace WhenAndWhere.Blazor.Authorization;

public class RoleAuthorizationRequirement : IAuthorizationRequirement
{
    public RoleAuthorizationRequirement(string roleName) => RoleName = roleName;
    
    public string RoleName { get; }
}