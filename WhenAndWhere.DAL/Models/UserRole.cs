using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WhenAndWhere.DAL.Models;

public class UserRole : IdentityUserRole<int>, IEntityLink
{
    public int FirstId { get; set; }

    [ForeignKey(nameof(FirstId))]
    public virtual User User { get; set; }

    public int SecondId { get; set; }

    [ForeignKey(nameof(SecondId))]
    public virtual Role Role { get; set; }

}

