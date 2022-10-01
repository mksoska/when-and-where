using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaShopDAL.Models;

public class Option
{
    public int Id { get; set; }
    public int MeetupId { get; set; }
    
    [ForeignKey(nameof(MeetupId))]
    public Meetup Meetup { get; set; }
    
    public int UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    
    public Address Address { get; set; }
    public String Label { get; set; }
    public DateTime Time { get; set; }
    public List<UserOption> UserOptions { get; set; }
}