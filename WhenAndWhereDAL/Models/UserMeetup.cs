using System.ComponentModel.DataAnnotations.Schema;
using PizzaShopDAL.Enums;

namespace PizzaShopDAL.Models;

public class UserMeetup
{
    public int UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    public int MeetupId { get; set; }
    
    [ForeignKey(nameof(MeetupId))]
    public Meetup Meetup { get; set; }
    
    public StateEnum State { get; set; }
    
    public DateTime DateInvited { get; set; }
}