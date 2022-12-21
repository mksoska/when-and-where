using System.ComponentModel.DataAnnotations;
using Autofac.Features.OwnedInstances;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using WhenAndWhere.DAL.Enums;

namespace WhenAndWhere.DAL.Models;

public class Role : IdentityRole<int>, IEntity
{
    public int MeetupId { get; set; }

    [ForeignKey(nameof(MeetupId))]
    public virtual Meetup Meetup { get; set; }

    public virtual List<UserRole> AssignedUsers { get; set; }
}