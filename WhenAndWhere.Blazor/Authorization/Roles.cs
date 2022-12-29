namespace WhenAndWhere.Blazor.Authorization;

public static class Roles
{
    public static readonly RoleAuthorizationRequirement Owner = new("Owner");
    public static readonly RoleAuthorizationRequirement User = new("User");
    public static readonly RoleAuthorizationRequirement Administrator = new("Administrator");
    public static readonly RoleAuthorizationRequirement Moderator = new("Moderator");
}