using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WhenAndWhere.DAL.Models;

public class User : IdentityUser<int>, IEntity
{
    [PersonalData]
    public string FirstName { get; set; }
    
    [PersonalData]
    public string Surname { get; set; }
    public byte[] Avatar { get; set; }

    public virtual List<Meetup> OwnedMeetups { get; set; }
    public virtual List<UserMeetup> InvitedMeetups { get; set; }
    public virtual List<UserOption> VotedOptions { get; set; }
    public virtual List<Option> CreatedOptions { get; set; }

    public virtual List<UserRole> AssignedRoles { get; set; }
}