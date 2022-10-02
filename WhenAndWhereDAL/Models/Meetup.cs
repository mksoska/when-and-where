using System.ComponentModel.DataAnnotations;
using PizzaShopDAL.Enums;

namespace PizzaShopDAL.Models;

public class Meetup
{
    [Key]
    public int Id { get; set; }
    public String Name { get; set; }
    public DateTime OptionFrom { get; set; }
    public DateTime OptionTo { get; set; }
    public byte[] Logo { get; set; }
    public MeetupType Type { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public virtual List<Option> Options { get; set; }
    public virtual List<UserMeetup> JoinnedUsers { get; set; }
}