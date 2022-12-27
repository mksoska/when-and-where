using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WhenAndWhere.DAL.Models;

public class UserRole : IdentityUserRole<int>
{
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }
    
    [ForeignKey(nameof(RoleId))]
    public virtual Role Role { get; set; }
}

