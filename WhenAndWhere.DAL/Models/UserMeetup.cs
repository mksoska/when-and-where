using System.ComponentModel.DataAnnotations.Schema;
using WhenAndWhere.DAL.Enums;

namespace WhenAndWhere.DAL.Models;

public class UserMeetup
{
    public int UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }

    public int MeetupId { get; set; }
    
    [ForeignKey(nameof(MeetupId))]
    public virtual Meetup Meetup { get; set; }
    
    public StateEnum State { get; set; }
    
    public DateTime DateInvited { get; set; }
}