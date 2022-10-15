using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WhenAndWhereDAL.Enums;

namespace WhenAndWhereDAL.Models;

public class Meetup : IEntity
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }
    public DateTime OptionsFrom { get; set; }
    public DateTime OptionsTo { get; set; }
    public byte[] Logo { get; set; }
    public MeetupType Type { get; set; }

    public int OwnerId { get; set; }

    [ForeignKey(nameof(OwnerId))]
    public virtual User Owner { get; set; }

    public virtual List<Option> Options { get; set; }

    public virtual List<Role> Roles { get; set; }

    public virtual List<UserMeetup> JoinedUsers { get; set; }
}