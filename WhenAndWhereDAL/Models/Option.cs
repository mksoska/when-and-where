using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhenAndWhereDAL.Models;

public class Option
{
    [Key]
    public int Id { get; set; }

    public int MeetupId { get; set; }
    
    [ForeignKey(nameof(MeetupId))]
    public virtual Meetup Meetup { get; set; }
    
    public int UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }

    public int AddressId { get; set; }

    [ForeignKey(nameof(AddressId))]
    public virtual Address Address { get; set; }

    public string Label { get; set; }
    public DateTime Time { get; set; }
    public virtual List<UserOption> UserOptions { get; set; }
}