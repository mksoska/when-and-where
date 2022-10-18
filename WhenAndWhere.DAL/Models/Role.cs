using System.ComponentModel.DataAnnotations;
using WhenAndWhere.DAL.Enums;

namespace WhenAndWhere.DAL.Models;

public class Role : IEntity
{
    [Key]
    public int Id { get; set; }

    public RoleEnum RoleName { get; set; }

    public virtual List<UserRole> AssignedUsers { get; set; }
}