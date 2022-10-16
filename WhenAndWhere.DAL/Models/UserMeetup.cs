using System.ComponentModel.DataAnnotations.Schema;
using WhenAndWhere.DAL.Enums;

namespace WhenAndWhere.DAL.Models;

public class UserMeetup : IEntityLink
{
    public int FirstId { get; set; }
    
    [ForeignKey(nameof(FirstId))]
    public virtual User User { get; set; }

    public int SecondId { get; set; }
    
    [ForeignKey(nameof(SecondId))]
    public virtual Meetup Meetup { get; set; }
    
    public StateEnum State { get; set; }
    
    public DateTime DateInvited { get; set; }
}