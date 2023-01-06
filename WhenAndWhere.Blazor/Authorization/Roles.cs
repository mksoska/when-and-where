namespace WhenAndWhere.Blazor.Authorization;

public static class Roles
{
    public const string Owner = nameof(Owner);
    public const string Administrator = nameof(Administrator);
    public const string Moderator = nameof(Moderator);
    public const string User = nameof(User);
    
    public static readonly RoleAuthorizationRequirement MeetupEdit = new(Owner, Administrator);
    public static readonly RoleAuthorizationRequirement MeetupView = new(Owner, Administrator, Moderator, User);
    public static readonly RoleAuthorizationRequirement ManageParticipants = new(Owner, Administrator, Moderator);
    public static readonly RoleAuthorizationRequirement ManageRoles = new(Owner, Administrator);
    public static readonly RoleAuthorizationRequirement ManageOptions = new(Owner, Administrator, Moderator);
    public static readonly RoleAuthorizationRequirement Vote = new(User);
}