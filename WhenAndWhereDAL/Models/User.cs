using System.ComponentModel.DataAnnotations;

namespace WhenAndWhereDAL.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public byte[] Avatar { get; set; }

    public virtual List<Meetup> OwnedMeetups { get; set; }
    public virtual List<UserMeetup> JoinedMeetups { get; set; }
    public virtual List<UserOption> VotedOptions { get; set; }
    public virtual List<Option> CreatedOptions { get; set; }

    public virtual List<UserRole> AssignedRoles { get; set; }
}