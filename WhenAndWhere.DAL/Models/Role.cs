using System.ComponentModel.DataAnnotations;
using Autofac.Features.OwnedInstances;
using System.ComponentModel.DataAnnotations.Schema;
using WhenAndWhere.DAL.Enums;

namespace WhenAndWhere.DAL.Models;

public class Role : IEntity
{
    [Key]
    public int Id { get; set; }

    public int MeetupId { get; set; }

    [ForeignKey(nameof(MeetupId))]
    public virtual Meetup Meetup { get; set; }

    public RoleEnum RoleName { get; set; }

    public virtual List<UserRole> AssignedUsers { get; set; }
}